using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction dropInput;
    public PlatformEffector2D platform;
    private bool isColliding;
    private bool isPressingDrop;
    private float lastDropTime;
    public float dropCoyoteTime; 
    private float originalArc = 125f;

    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable() {
        dropInput = playerControls.Player.Drop;
        // dropInput.started += Drop;
        dropInput.performed += OnDrop;
        dropInput.canceled += OnDropEnd;
        dropInput.Enable();
    }

    private void OnDisable() {
        dropInput.Disable();
    }

    private void Update() {
        // Update timers
        if (isPressingDrop) {
            lastDropTime = dropCoyoteTime;
        }
        else {
            lastDropTime -= Time.deltaTime;
        }

        // Perform drop
        if (isColliding && lastDropTime > 0) {
            platform.surfaceArc = 0;
            StartCoroutine(Wait());
        }
    }

    public void OnDrop(InputAction.CallbackContext context) { isPressingDrop = true; }
    public void OnDropEnd(InputAction.CallbackContext context) { isPressingDrop = false; }

    private void OnCollisionEnter2D(Collision2D collision) { isColliding = true; }
    private void OnCollisionExit2D(Collision2D collision) { isColliding = false; }

    IEnumerator Wait() { 
        yield return new WaitForSeconds(0.3f); 
        platform.surfaceArc = originalArc;
    }
}
