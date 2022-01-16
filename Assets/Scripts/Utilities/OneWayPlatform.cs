using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    private bool isColliding;
    private bool isPressingDrop;
    private float lastDropTime;
    private GameObject currentPlatform;
    private CapsuleCollider2D playerCollider;
    public float dropCoyoteTime;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        // Update timers
        if (isPressingDrop) lastDropTime = dropCoyoteTime;
        else lastDropTime -= Time.deltaTime;

        // Perform drop
        if (isColliding && lastDropTime > 0 && currentPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    public void DropCallback(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                {
                    OnDrop();
                    break;
                }
            case InputActionPhase.Canceled:
                {
                    OnDropEnd();
                    break;
                }
            default: break;
        }
    }

    private void OnDrop() { isPressingDrop = true; }
    private void OnDropEnd() { isPressingDrop = false; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.gameObject;
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
            isColliding = false;
        }
    }

    IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
