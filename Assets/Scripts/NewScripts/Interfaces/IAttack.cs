using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    public interface IAttack 
    {
        public void HandleAttackInput(InputAction.CallbackContext context);
    }
}
