using System;
using System.Collections;
using System.Collections.Generic;
using _Main._Scripts;
using _Main._Scripts.SaveLoad;
using _Main._Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
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
        DontDestroyOnLoad(gameObject);
        _gameData = SaveManager.Load<GameData>();
    }

    private void Start()
    {
        SetState(GameState.Starting);
        foreach (var powerUp in powerUps)
        {
            powerUp.SetActive(_gameData.showItems);
        }
    }

    public void StageReset() => stageReset?.Invoke();

    public void ReloadScene()
    {
        SceneManager.LoadScene("Test");
        _gameData = SaveManager.Load<GameData>();
    }

    private void OnDestroy()
    {
        SaveManager.Save(_gameData);
    }

    public void SetState(GameState newState)
    {
        if(currentState==newState) return;
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);

    }


}
