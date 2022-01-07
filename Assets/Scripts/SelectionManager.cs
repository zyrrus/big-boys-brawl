using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SelectionState { Map, Character }

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    public static event Action<SelectionState> OnStateChanged;
    public SelectionState State;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        UpdateState(SelectionState.Map);
    }

    public void UpdateState(SelectionState newState)
    {
        State = newState;

        switch (newState)
        {
            case SelectionState.Map:
                HandleMapSelection();
                break;
            case SelectionState.Character:
                HandleCharacterSelection();
                break;
            default: throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleMapSelection() {}
    private void HandleCharacterSelection() {}
}
 