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
        GameObject UIParent = GameObject.FindGameObjectWithTag("SelectionParent");
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

        selectionIndex += Mathf.RoundToInt(context.ReadValue<float>());
        selectionScript.SetImage(selectionIndex + "");
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
