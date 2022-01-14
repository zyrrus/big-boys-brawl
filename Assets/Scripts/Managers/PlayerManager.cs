using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject playerController;
    private PlayerMovement moveScript;
    private PlayerInput playerInput;
    private GameObject UIParent;

    [SerializeField] private GameObject selectionCardPrefab;
    [SerializeField] private PanelSelection selectionScript;
    [SerializeField] private int selectionIndex;
    [SerializeField] private bool isReady;

    private void Start()
    {
        // Reset variables
        isReady = false;
        selectionIndex = 0;

        // Add self to GameManager
        gm = GameManager.Instance;
        gm.PlayerJoined(gameObject);

        // Create new card
        UIParent = GameObject.FindGameObjectWithTag("SelectionParent");
        GameObject newCard = Instantiate(selectionCardPrefab, Vector3.zero, Quaternion.identity);
        newCard.transform.SetParent(UIParent.transform);

        // Get access to selection panel controller
        selectionScript = newCard.GetComponent<PanelSelection>();

        // Get access to the player controller
        playerController = transform.GetChild(0).gameObject;
        moveScript = playerController.GetComponent<PlayerMovement>();
        playerInput = playerController.GetComponent<PlayerInput>();
    }

    public void Ready(InputAction.CallbackContext context) 
    { 
        if (context.phase != InputActionPhase.Performed) return;

        isReady = true; 
        selectionScript.SetReady(true);

        gm.OnPlayerReady();
    }

    public void SetSelection(InputAction.CallbackContext context) 
    {
        if (isReady || context.phase != InputActionPhase.Performed) return;

        int charLength = GameManager.Instance.allCharacters.Length;

        selectionIndex += Mathf.RoundToInt(context.ReadValue<float>());
        if (selectionIndex < 0) selectionIndex += charLength; 
        else selectionIndex %= charLength;

        selectionScript.SetImage(selectionIndex);
    }


    public void OnGameStart() 
    {
        // Load character
        GameObject selectedChar = Instantiate(gm.allCharacters[selectionIndex], playerController.transform.position, playerController.transform.rotation);
        selectedChar.transform.SetParent(playerController.transform);

        // Update controller
        SetInputAction("Player");
        SetCanMove(true);

    }

    // Change input action to player or selection
    public void SetInputAction(string method) 
    {
        playerInput.SwitchCurrentActionMap(method);
    }

    private void SetCanMove(bool value) 
    {
        moveScript.canMove = value;
    }
}
