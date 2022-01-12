using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState { Select, Play, Pause, Outcome }

public class GameManager : Singleton<GameManager>
{
    public static string CharactersPath = "Characters";
    public static string MapsPath = "Maps";
    public static event Action<GameState> OnStateChanged;
    public static GameState State;
    private PlayerInputManager pim;

    public UnityEngine.Object[] allCharacters;
    public UnityEngine.Object[] allMaps;
    private List<GameObject> players;
    private int playerCount;
    private int readyCount;

    private void Start()
    {
        allCharacters = Resources.LoadAll(CharactersPath, typeof(GameObject));
        allMaps = Resources.LoadAll(MapsPath, typeof(GameObject));

        // Debug.Log(allCharacters.Length);
        // foreach (GameObject character in allCharacters)
        //     Debug.Log(character);

        players = new List<GameObject>();
        pim = GetComponent<PlayerInputManager>();

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
        pim.EnableJoining();
        
    }

    private void HandlePlay() {
        pim.DisableJoining();
        Debug.Log("PLAYING GAME");
    }

    private void HandlePause() {}

    private void HandleOutcome() {}
    
    public void PlayerJoined(GameObject player) 
    {
        players.Add(player);
        playerCount++;
    }

    public void PlayerLeft() {}

    public void OnPlayerReady() 
    {
        readyCount++;
        CheckReadyToStart();
    }

    private void CheckReadyToStart() 
    {
        if (readyCount == playerCount) 
            UpdateState(GameState.Play);
    }

}
