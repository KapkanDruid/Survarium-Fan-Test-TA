using Assets.Scripts.Architecture.CustomEventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using IDisposable = Assets.Scripts.Architecture.CustomEventBus.IDisposable;

namespace Assets.Scripts.Architecture.Input
{
    public class InputEventHandler : IDisposable
    {
        private EventBus _eventBus;
        private InputSystem_Actions _inputActions;

        private InputMoveVectorSignal _moveVectorSignal;
        private InputLookVectorSignal _lookVectorSignal;

        public InputEventHandler(EventBus eventBus, InputSystem_Actions inputActions)
        {
            _eventBus = eventBus;
            _inputActions = inputActions;
        }

        public void Initialize()
        {
            _inputActions.Enable();

            _inputActions.Player.Move.performed += ReadMoveVector;
            _inputActions.Player.Move.canceled += ReadMoveVector;

            _inputActions.Player.Jump.performed += context => ReadJump();

            _inputActions.Player.Look.performed += ReadLookVector;
            _inputActions.Player.Look.canceled += ReadLookVector;

            _inputActions.Player.Sprint.performed += context => ReadRun();
            _inputActions.Player.Sprint.canceled += context => ReadRun();
        }

        private void ReadMoveVector(InputAction.CallbackContext context)
        {
            var moveVector = context.ReadValue<Vector2>();

            if (_moveVectorSignal == null)
                _moveVectorSignal = new InputMoveVectorSignal(moveVector);
            else 
                _moveVectorSignal.SetValue(moveVector);

            _eventBus.Invoke(_moveVectorSignal);
        }

        private void ReadJump()
        {
            _eventBus.Invoke(new InputJumpSignal());
        }

        private void ReadRun()
        {
            _eventBus.Invoke(new InputRunSignal());
        }

        private void ReadLookVector(InputAction.CallbackContext context)
        {
            var lookVector = context.ReadValue<Vector2>();

            if (_lookVectorSignal == null)
                _lookVectorSignal = new InputLookVectorSignal(lookVector);
            else
                _lookVectorSignal.SetValue(lookVector);

            _eventBus.Invoke(_lookVectorSignal);
        }

        public void Dispose()
        {
            _inputActions.Disable();

            _inputActions.Player.Move.performed -= ReadMoveVector;
            _inputActions.Player.Move.canceled -= ReadMoveVector;

            _inputActions.Player.Jump.performed -= context => ReadJump();

            _inputActions.Player.Look.performed -= ReadLookVector;
            _inputActions.Player.Look.canceled -= ReadLookVector;

            _inputActions.Player.Sprint.performed -= context => ReadRun();
            _inputActions.Player.Sprint.canceled -= context => ReadRun();
        }
    }
}
