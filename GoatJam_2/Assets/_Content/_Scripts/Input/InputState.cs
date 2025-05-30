using UnityEngine.InputSystem;

namespace YInput
{
    public class InputState
    { 
        public bool IsPressed { get; private set; }
        public bool IsHeld { get; private set; }
        public bool IsReleased { get; private set; }

        public InputState(InputAction inputAction)
        {
            inputAction.started += ctx => OnStarted();
            inputAction.performed += ctx => OnPerformed();
            inputAction.canceled += ctx => OnCanceled();
        }

        public InputState(InputAction inputAction, InputAction inputAction2)
        {
            inputAction.started += ctx => OnStarted();
            inputAction.performed += ctx => OnPerformed();
            inputAction.canceled += ctx => OnCanceled();

            inputAction2.started += ctx => OnStarted();
            inputAction2.performed += ctx => OnPerformed();
            inputAction2.canceled += ctx => OnCanceled();
        }

        private void OnStarted()
        {
            IsHeld = true;
            IsPressed = true;
        }

        private void OnPerformed() => IsPressed = true;

        private void OnCanceled()
        {
            IsHeld = false;
            IsPressed = false;
            IsReleased = true;
        }

        public void ResetInputInfo()
        {
            IsPressed = false;
            IsReleased = false;
        }
    }
}