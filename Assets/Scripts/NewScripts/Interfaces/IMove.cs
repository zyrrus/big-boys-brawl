using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    public interface IMove
    {
	    public void HandleMoveInput(InputAction.CallbackContext context);
    }
}
