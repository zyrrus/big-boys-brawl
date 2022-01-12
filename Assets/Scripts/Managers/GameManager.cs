using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState { Select, Play, Pause, Outcome }

public class GameManager : Singleton<GameManager>
{
    // PlayerInputManager
    private List<GameObject> players = new List<GameObject>();
    // PlayerInputManager.Enable/DisableJoining()
    public static event Action<GameState> OnStateChanged;
    public static GameState State;

    public void PlayerJoined(GameObject player) 
    {
        players.Add(player);
    }

    public void PlayerLeft() {}

    private void Start()
    {
        UpdateState(GameState.Select);
    }

    public void UpdateState(GameState newState) 
    {
        if (State == newState) return;

        State = newState;
        switch (newState)
        {
            case GameState.Select:
                HandleSelect();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.Outcome:
                HandleOutcome();
                break;
            default: throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleSelect() {
        // playerinputmanager
    }

    private void HandlePlay() {}

    private void HandlePause() {}

    private void HandleOutcome() {}
}
