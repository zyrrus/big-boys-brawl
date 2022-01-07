using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Selection, PlayGame, Outcome }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnStateChanged;
    public GameState State;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateState(GameState.MainMenu);
    }

    public void UpdateState(GameState newState) 
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Selection:
                HandleSelection();
                break;
            case GameState.PlayGame:
                HandlePlayGame();
                break;
            case GameState.Outcome:
                HandleOutcome();
                break;
            default: throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnStateChanged?.Invoke(newState);
    }
    
    private void HandleMainMenu() { 
        // On start:
        // - Load menu scene
    }
    private void HandleSelection() { 
        // On menu click 'Play':
        // - Load selection screen
        // - Define Map, Players, and Characters
        // - Swap to 'PlayGame' 
    }
    private void HandlePlayGame() { 
        // Disable Selection screens, enable arena 
        // (maybe do this with scenes)
        // Might need to map player inputs 
        // to each selected character
    }
    private void HandleOutcome() { 
        // Disable play
        // Enable Outcome screen
    }
}
