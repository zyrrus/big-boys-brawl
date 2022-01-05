using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("References")]
	private Rigidbody2D rb;
    private PlayerInputActions playerControls;
    private InputAction controllerInput;

	[Header("Movement")]
	public float moveSpeed;
	public float acceleration;
	public float deceleration;
	public float airAccel; //multiplier when player in air
	public float airDecel; //multiplier when player in air
	public float velPower;
	[Space(10)]
	public float frictionAmount;
	[Space(10)]
	private Vector2 moveInput;
	private Vector2 lastMoveInput;
	public bool canMove = true;

	[Header("Jump")]
	public float jumpForce;
	[Range(0, 1)]
	public float jumpCutMultiplier;
	[Space(5)]
	public float fallGravityMultiplier;
	private float gravityScale;
	[Space(5)]
	private bool isJumping;
	private bool jumpInputReleased = true;

	[Header("Checks")]
	public Transform groundCheckPoint;
	public Vector2 groundCheckSize;
	[Space(10)]
	public float jumpCoyoteTime;
	private float lastGroundedTime;
	[Space(5)]
	public float jumpBufferTime;
	private float lastJumpTime;
	[Space(10)]
	private bool isFacingRight = true; //sometimes I like to make a ReadOnly attribute to display private varibles like this, allowing for info to be layed out nicer in the inspector

	[Header("Layers & Tags")]
	public LayerMask groundLayer;

    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable() {
        controllerInput = playerControls.Player.Move;
        controllerInput.Enable();

        playerControls.Player.Jump.started += Jump;
        playerControls.Player.Jump.performed += OnJump;
        playerControls.Player.Jump.canceled += OnJumpEnd;
        playerControls.Player.Jump.Enable();
    }

    private void OnDisable() {
        controllerInput.Disable();
        playerControls.Player.Jump.Disable();
    }

	private void Start()
	{
		/*
		 - retrieves rigidbody
		 - if you want the player to have more functionality in future eg: combat, more movement options, etc. 
		 - I would recommend creating a seperate Player Class and using that to hold all player info such as the rigidbody and make seperate movement, combat, classes for specific functions
		 - Highly recommed looking into abstacrtion, decoupling and inheritance if you're working on a large project
		*/
		rb = GetComponent<Rigidbody2D>();

		gravityScale = rb.gravityScale;
	}

	private void Update()
	{
		#region Inputs
		moveInput = controllerInput.ReadValue<Vector2>();
		#endregion

		#region Run
		if (moveInput.x != 0)
			lastMoveInput.x = moveInput.x;
		if (moveInput.y != 0)
			lastMoveInput.y = moveInput.y;

		if ((lastMoveInput.x > 0 && !isFacingRight) || (lastMoveInput.x < 0 && isFacingRight))
		{
			Turn();
			isFacingRight = !isFacingRight;
		}
		#endregion

		#region Ground
		//checks if set box overlaps with ground
		if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer) && !isJumping) 
		{
			//resets countdown timer
			lastGroundedTime = jumpCoyoteTime; 
		}
		#endregion

		#region Jump
		//checks if the player is grounded or falling and that they have released jump
		if (rb.velocity.y <= 0)
		{
			//if so we are no longer jumping and could jump again
			isJumping = false;
		}

		#endregion

		#region Timer
		lastGroundedTime -= Time.deltaTime;
		lastJumpTime -= Time.deltaTime;
		#endregion
	}

	private void FixedUpdate()
	{
		#region Run
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
		
		#endregion

		#region Friction
		//check if we're grounded and that we are trying to stop (not pressing forwards or backwards)
		if (lastGroundedTime > 0 && !isJumping && Mathf.Abs(moveInput.x) < 0.01f) 
		{
			//then we use either the friction amount (~ 0.2) or our velocity
			float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
			//sets to movement direction
			amount *= Mathf.Sign(rb.velocity.x);
			//applies force against movement direction
			rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
		}
		#endregion

		#region Jump Gravity
		if (rb.velocity.y < 0 && lastGroundedTime <= 0)
		{
			rb.gravityScale = gravityScale * fallGravityMultiplier;
		}
		else
		{
			rb.gravityScale = gravityScale;
		}
		#endregion
	}

	#region Jump
	private void Jump(InputAction.CallbackContext context)
	{
        if (lastJumpTime <= 0 && !isJumping && jumpInputReleased)
		{
			if (lastGroundedTime > 0)
			{
				lastGroundedTime = 0;
                //apply force, using impluse force mode
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                lastJumpTime = 0;
                isJumping = true;
                jumpInputReleased = false;
                lastJumpTime = jumpBufferTime;
            }
        }
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		lastJumpTime = jumpBufferTime;
		jumpInputReleased = false;
	}

	public void OnJumpEnd(InputAction.CallbackContext context)
	{
		if (rb.velocity.y > 0 && isJumping)
		{
			//reduces current y velocity by amount (0 - 1)
			rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
		}

		jumpInputReleased = true;
		lastJumpTime = 0;
	}
	#endregion

	private IEnumerator StopMovement(float duration)
	{
		canMove = false;
		yield return new WaitForSeconds(duration);
		canMove = true;
	}

	private void Turn()
	{
		//stores scale and flips x axis, flipping the entire gameObject (could also rotate the player)
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
	}

}
