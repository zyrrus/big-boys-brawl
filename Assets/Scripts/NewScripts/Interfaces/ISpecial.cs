using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBoysBrawl
{
    public interface ISpecial
    {
        public void HandleSpecialInput(InputAction.CallbackContext context);
    }
}
