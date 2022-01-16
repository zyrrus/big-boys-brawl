using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    public class BasicMove : MonoBehaviour, IMove
    {
        private Rigidbody2D rb;
        private Vector2 moveInput;
        private Vector2 lastMoveInput;
        private float lastGroundedTime;
        private bool isFacingRight = true; //sometimes I like to make a ReadOnly attribute to display private varibles like this, allowing for info to be layed out nicer in the inspector

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                //calculate the direction we want to move in and our desired velocity
                float targetSpeed = moveInput.x * moveSpeed;
                //calculate difference between current velocity and desired velocity
                float speedDif = targetSpeed - rb.velocity.x;

                //change acceleration rate depending on situation
                float accelRate;
                if (lastGroundedTime > 0)
                {
                    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
                }
                else
                {
                    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * airAccel : deceleration * airDecel;
                }

                //applies acceleration to speed difference, the raises to a set power so acceleration increases with higher speeds
                //finally multiplies by sign to reapply direction
                float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

                //applies force force to rigidbody, multiplying by Vector2.right so that it only affects X axis 
                rb.AddForce(movement * Vector2.right);
            }
        }

        public void HandleMoveInput(InputAction.CallbackContext context) 
        {
            moveInput = context.ReadValue<Vector2>();

            if (moveInput.x != 0) lastMoveInput.x = moveInput.x;
            if (moveInput.y != 0) lastMoveInput.y = moveInput.y;

            if ((lastMoveInput.x > 0 && !isFacingRight) || 
                (lastMoveInput.x < 0 && isFacingRight))
            {
                FlipHorizontal();
                isFacingRight = !isFacingRight;
            }
        }

        private void FlipHorizontal()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
