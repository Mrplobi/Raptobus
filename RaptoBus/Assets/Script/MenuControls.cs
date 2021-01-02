using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RaptoBus
{
    public class MenuControls : MonoBehaviour
    {
        public InputActionMap menuControls;

        private void Awake()
        {
            menuControls["Validate"].performed += Validate;
            menuControls["Pause"].performed += Pause;
        }

        private void OnEnable()
        {
            menuControls.Enable();
        }

        private void Pause(InputAction.CallbackContext obj)
        {
            GameManager.Instance.PauseGame();
        }

        private void Validate(InputAction.CallbackContext obj)
        {
            UIManager.Instance.ChangeState(obj);
        }
    }
}
