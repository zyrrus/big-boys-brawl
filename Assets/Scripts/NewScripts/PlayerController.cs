using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class PlayerController : MonoBehaviour 
    {
        private Rigidbody2D rb;
        private IMove moveController;
        private IJump jumpController;
        private IAttack attackController;
        private ISpecial specialController;

        private void Awake()
        {
            moveController = GetComponent<IMove>();
            jumpController = GetComponent<IJump>();
            attackController = GetComponent<IAttack>();
            specialController = GetComponent<ISpecial>();

            Debug.Log(moveController);

            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            Debug.Log("Updating PC");
        }

        #region Controllers

        public void HandleMoveInput(InputAction.CallbackContext context) 
        {
            moveController.HandleMoveInput(context);
        }

        public void HandleJumpInput(InputAction.CallbackContext context) 
        {
            jumpController.HandleJumpInput(context);
        }

        public void HandleAttackInput(InputAction.CallbackContext context) 
        {
            attackController.HandleAttackInput(context);
        }

        public void HandleSpecialInput(InputAction.CallbackContext context) 
        {
            specialController.HandleSpecialInput(context);
        }

        #endregion
    }
}