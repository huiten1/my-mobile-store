using System;
using System.Collections;
using System.Collections.Generic;
using _Main._Scripts;
using _Main._Scripts.SaveLoad;
using _Main._Scripts.Utils;
using GameAnalyticsSDK;
using Lib.GameEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameEvent restartClicked;
    [SerializeField] private GameEvent nextClicked;
    [SerializeField] private GameEvent allUnlocked;
    [SerializeField] private GameObject[] powerUps;
    private GameData _gameData;
    public GameData GameData => _gameData;
    public enum GameState
    {
        Starting,
        Ending,
        Victory,
        Fail,
        Paused
    }
    private GameState currentState;
    public GameState CurrentState => currentState;
    public event Action<GameState> OnGameStateChanged;

    public event Action stageReset;
    private void Awake()
    {
        GameAnalytics.Initialize();
        // DontDestroyOnLoad(gameObject);
        _gameData = SaveManager.Load<GameData>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        restartClicked.AddListener(ReloadScene);
        allUnlocked.AddListener(LevelComplete);
        nextClicked.AddListener(ReloadScene);
        SetState(GameState.Starting);
        foreach (var powerUp in powerUps)
        {
            powerUp.SetActive(_gameData.showItems);
        }

        yield return null;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,$"Level_{_gameData.level}");
    }

    public void LevelComplete()
    {
        _gameData.level++;
        SaveManager.Save(_gameData);
        StageReset();
        SetState(GameState.Victory);

        if (currentState != GameState.Victory)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,$"Level_{_gameData.level}");
        }
    }

    public void StageReset() => stageReset?.Invoke();

    public void ReloadScene()
    {
        SceneManager.LoadScene("Test");
        _gameData = SaveManager.Load<GameData>();
        SetState(GameState.Starting);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,$"Level_{_gameData.level}");
    }

    private void OnDestroy()
    {
        restartClicked.RemoveListener(ReloadScene);
        allUnlocked.RemoveListener(LevelComplete);
        nextClicked.RemoveListener(ReloadScene);
        SaveManager.Save(_gameData);
    }

    public void SetState(GameState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);

    }


}
