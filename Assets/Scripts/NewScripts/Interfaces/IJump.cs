using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    public interface IJump
    {
        public void HandleJumpInput(InputAction.CallbackContext context);
    }
}
