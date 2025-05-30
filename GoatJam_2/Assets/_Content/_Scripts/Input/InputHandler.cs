using UnityEngine;
using Yaygun.Utilities.Singleton;

namespace YInput
{
    public enum EInputMod { Gameplay, UI, None}
    public class InputHandler : Singleton<InputHandler>
    {

        #region inputs

        //public Vector2 MoveInput { get; private set; }

        //public Vector2 MousePositionChange { get; private set; }
        
        public float HorizontalMove { get; private set; }


        public InputState LeftClick { get; private set; }

        public InputState Interact { get; private set; }

        public InputState Jump { get; private set; }

        public InputState EscGameplay { get; private set; }

        public InputState EscUI { get; private set; }

        #endregion

        private Keys _keys;

        private EInputMod _inputMod;

        private EInputMod _inputModBeforePause;

        public float MouseSensitivity { get; private set; } = 1f;
        protected override void Initialize()
        {
            _keys = new Keys();

            LeftClick = new InputState(_keys.gameplay.LeftClick);
            Interact = new InputState(_keys.gameplay.Interact); 
            
            Jump = new InputState(_keys.gameplay.Jump);
            EscGameplay = new InputState(_keys.gameplay.Esc);
            EscUI = new InputState(_keys.ui.Esc);

            //EventHub.Ev_LevelStartInputMod += OnLevelStartInputMod;

            EnableGameplayMod();
        }

        protected override void Destruction()
        {
            if (Instance != this)
                return;

            //EventHub.Ev_LevelStartInputMod -= OnLevelStartInputMod;
            _keys.Disable();
        }
        private void Update()
        {
            //MoveInput = _keys.gameplay.move.ReadValue<Vector2>();
            //MousePositionChange = _keys.gameplay.MouseMove.ReadValue<Vector2>() * MouseSensitivity;
            HorizontalMove = _keys.gameplay.HorizontalInput.ReadValue<float>();
        }
        private void LateUpdate()
        {
            LeftClick.ResetInputInfo();
            Interact.ResetInputInfo();
            Jump.ResetInputInfo();
            EscGameplay.ResetInputInfo();
            EscUI.ResetInputInfo();
        }

        public void EnableGameplayMod()
        {
            _keys.gameplay.Enable();
            _keys.ui.Disable();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            _inputMod = EInputMod.Gameplay;
        }
        public void EnableUIMod()
        {
            _keys.gameplay.Disable();
            _keys.ui.Enable();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _inputMod = EInputMod.UI;
        }

        public void EnableNoInputMod()
        {
            _keys.gameplay.Disable();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;

            _inputMod = EInputMod.None;
        }

        public void SetMouseSensitivity(float sensitivity)
        {
            MouseSensitivity = sensitivity;
        }

        private void OnLevelStartInputMod(EInputMod inputMode)
        {
            //OpenInputMode(inputMode);
        }

        public void Pause()
        {
            _inputModBeforePause = _inputMod;
            EnableUIMod();
        }

        public void Resume()
        {
            OpenInputMode(_inputModBeforePause);
        }

        private void OpenInputMode(EInputMod newInputMod)
        {
            switch (newInputMod)
            {
                case EInputMod.Gameplay:
                    EnableGameplayMod();
                    break;
                case EInputMod.UI:
                    EnableUIMod();
                    break;
                case EInputMod.None:
                    EnableNoInputMod();
                    break;
            }
        }
    }
}
