using Assets.Scripts.Architecture.CustomEventBus;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerVerticalMoveHandler : ITickable, System.IDisposable
    {
        private EventBus _eventBus;
        private PlayerData _playerData;
        private CharacterController _characterController;
        private PlayerHorizontalMoveHandler _playerHorizontalMoveHandler;

        private float _verticalVelocity;
        private float _gravityForce;
        private float _jumpVelocity;

        public PlayerVerticalMoveHandler(EventBus eventBus, 
            PlayerData playerData, 
            CharacterController characterController, 
            PlayerHorizontalMoveHandler pLayerHorizontalMoveHandler)
        {
            _eventBus = eventBus;
            _playerData = playerData;
            _characterController = characterController;
            _playerHorizontalMoveHandler = pLayerHorizontalMoveHandler;

            _eventBus.Subscribe<InputJumpSignal>(OnJumpCalled);

            SetJumpVelocity();
        }

        public void Tick()
        {
            _playerHorizontalMoveHandler.SetYVelocity(_verticalVelocity);

            if (!_characterController.isGrounded)
                _verticalVelocity -= _gravityForce * Time.deltaTime;
            else
                _verticalVelocity = -0.001f;
        }

        private void SetJumpVelocity()
        {
            float maxHeightTime = _playerData.JumpTime / 2;
            _gravityForce = (2 * _playerData.JumpHeight) / Mathf.Pow(maxHeightTime, 2);
            _jumpVelocity = (2 * _playerData.JumpHeight) / maxHeightTime;
        }

        private void OnJumpCalled(InputJumpSignal signal)
        {
            if (_characterController.isGrounded)
            {
               SetJumpVelocity();
                _verticalVelocity = _jumpVelocity;

                _playerHorizontalMoveHandler.SetYVelocity(_verticalVelocity);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputJumpSignal>(OnJumpCalled);
        }
    }
}
