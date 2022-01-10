using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, SelectMap, SelectCharacter, PlayGame, Outcome }

public class GameManager : PersistentSingleton<GameManager>
{
    public static event Action<GameState> OnStateChanged;
    public GameState State;

    private void Start()
    {
        UpdateState(GameState.MainMenu);
    }

    public void UpdateState(GameState newState) 
    {
        if (State == newState) return;

        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.SelectMap:
                HandleSelectMap();
                break;
            case GameState.SelectCharacter:
                HandleSelectCharacter();
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
        // 
    }
    private void HandleSelectMap() { 
        // On menu click 'Play':
        // - Load map selection screen
        // - Define Map, Players, and Characters
        // - Swap to 'PlayGame' 
    }
    private void HandleSelectCharacter() { 
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
