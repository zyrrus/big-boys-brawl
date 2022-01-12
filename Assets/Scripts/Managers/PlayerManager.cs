using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerController;
    private PlayerMovement moveScript;
    private PlayerInput playerInput;

    [SerializeField] private GameObject selectionCard;
    [SerializeField] private int selectionIndex;
    [SerializeField] private bool isReady;

    private void Start()
    {
        // Reset variables
        isReady = false;
        selectionIndex = 0;

        // Add self to GameManager
        GameManager.Instance.PlayerJoined(gameObject);

        // Create new card
        GameObject UIParent = GameObject.FindGameObjectWithTag("SelectionParent");
        GameObject newCard = Instantiate(selectionCard, Vector3.zero, Quaternion.identity);
        newCard.transform.SetParent(UIParent.transform);

        // Get access to the player controller
        playerController = transform.GetChild(0).gameObject;
        moveScript = playerController.GetComponent<PlayerMovement>();
        playerInput = playerController.GetComponent<PlayerInput>();
    }

    public void Ready() { isReady = true; }

    public void SetSelection(InputAction.CallbackContext context) 
    {
        selectionIndex += Mathf.RoundToInt(context.ReadValue<float>());
    }

    public void SetInputMethod(string method) 
    {

    }

    public void OnGameStart() 
    {
        // Load sprite using selection index
        // set input method to player
    }

    private void setCanMove(bool value) 
    {

    }
}
