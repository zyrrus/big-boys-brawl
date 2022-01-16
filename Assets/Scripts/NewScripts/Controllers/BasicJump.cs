using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    public class BasicJump : MonoBehaviour, IJump
    {
        private Rigidbody2D rb;

    public float jumpForce;
        [Range(0, 1)] public float jumpCutMultiplier;
        private bool isJumping;
        private bool jumpInputReleased = true;
        private float lastGroundedTime;
        [SerializeField] private float jumpBufferTime;
        private float lastJumpTime;

        public void HandleJumpInput(InputAction.CallbackContext context) 
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    {
                        if (!isJumping && 
                            lastJumpTime <= 0 && 
                            lastGroundedTime > 0 && 
                            jumpInputReleased)
                        {
                            // Apply jumping force
                            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                            lastGroundedTime = 0;
                            lastJumpTime = 0;
                            isJumping = true;
                            jumpInputReleased = false;
                            lastJumpTime = jumpBufferTime;
                        }
                        break;
                    }
                case InputActionPhase.Performed:
                    {
                        // On Jump Performed
                        lastJumpTime = jumpBufferTime;
                        jumpInputReleased = false;
                        break;
                    }
                case InputActionPhase.Canceled:
                    {
                        // On Jump End
                        jumpInputReleased = true;
                        lastJumpTime = 0;
                        
                        if (rb.velocity.y > 0 && isJumping)
                            rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
                        break;
                    }
                default: break;
            }
        }
    }
}
