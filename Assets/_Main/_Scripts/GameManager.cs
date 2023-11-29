using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Starting,
        Ending,
        Victory,
        Fail,
        Paused
    }

    public GameState gameState;
    public static event Action<GameState> OnGameStateChanged;



    private void Awake()
    {
        Instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.Starting:
                break;

            case GameState.Ending:
                break;

            case GameState.Victory:
                break;

            case GameState.Fail:
                break;

            case GameState.Paused:
                break;

            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);

    }


}
