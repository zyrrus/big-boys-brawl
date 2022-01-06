using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    private bool isColliding;
    private bool isPressingDrop;
    private float originalArc = 125f;
    private float lastDropTime;
    public float dropCoyoteTime; 
    private GameObject currentPlatform;
    private PlatformEffector2D platformEffector;

    private void Update() {
        // Update timers
        if (isPressingDrop) {
            lastDropTime = dropCoyoteTime;
        }
        else {
            lastDropTime -= Time.deltaTime;
        }

        if (lastDropTime > 0)
            Debug.LogFormat("{0}", lastDropTime);

        // Debug.LogFormat("colliding: {0}, lastDropTime {1}, currentPlatform {2}", isColliding, lastDropTime, currentPlatform);
        // Perform drop
        if (isColliding && lastDropTime > 0 && currentPlatform != null) {
            StartCoroutine(DisableCollision());
        }
    }

    public void DropCallback(InputAction.CallbackContext context) {
        switch(context.phase) {
            case InputActionPhase.Started: {
                OnDrop();
                break;
            }
            case InputActionPhase.Canceled: {
                OnDropEnd();
                break;
            }
            default: break;
        }
    }

    private void OnDrop() { isPressingDrop = true; }
    private void OnDropEnd() { isPressingDrop = false; }

    private void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.CompareTag("Platform")) {
            currentPlatform = collision.gameObject;
            isColliding = true;
        } 
    }

    private void OnCollisionExit2D(Collision2D collision) { 
        if (collision.gameObject.CompareTag("Platform")) {
            currentPlatform = null;
            isColliding = false;
        } 
    }

    IEnumerator DisableCollision() { 
        platformEffector = currentPlatform.GetComponent<PlatformEffector2D>();
        platformEffector.surfaceArc = 0;
        yield return new WaitForSeconds(0.3f); 
        platformEffector.surfaceArc = originalArc;
    }
}
