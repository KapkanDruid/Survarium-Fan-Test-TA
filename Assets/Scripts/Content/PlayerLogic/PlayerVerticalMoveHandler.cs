﻿using Assets.Scripts.Architecture.CustomEventBus;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerVerticalMoveHandler : ITickable, System.IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly PlayerData _playerData;
        private readonly CharacterController _characterController;
        private readonly PlayerHorizontalMoveHandler _playerHorizontalMoveHandler;
        private readonly PlayerCrouchHandler _playerCrouchHandler;

        private float _verticalVelocity;
        private float _gravityForce;
        private float _jumpVelocity;

        public PlayerVerticalMoveHandler(EventBus eventBus,
            PlayerData playerData,
            CharacterController characterController,
            PlayerHorizontalMoveHandler pLayerHorizontalMoveHandler,
            PlayerCrouchHandler playerCrouchHandler = null)
        {
            _eventBus = eventBus;
            _playerData = playerData;
            _characterController = characterController;
            _playerHorizontalMoveHandler = pLayerHorizontalMoveHandler;
            _playerCrouchHandler = playerCrouchHandler;

            _eventBus.Subscribe<InputJumpSignal>(OnJumpCalled);

            SetJumpVelocity();
        }

        public void Tick()
        {
            _playerHorizontalMoveHandler.AddVelocity(new(0, _verticalVelocity, 0));

            CheckCelling();

            if (!_characterController.isGrounded)
                _verticalVelocity -= _gravityForce * Time.deltaTime;
            else
                _verticalVelocity = -0.001f;
        }

        private void CheckCelling()
        {
            if ((_characterController.collisionFlags & CollisionFlags.Above) != 0 && _verticalVelocity > 0)
                _verticalVelocity = 0;
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
                if (_playerCrouchHandler?.IsCrouching == true)
                {
                    _playerCrouchHandler.Stand();
                    return;
                }

                SetJumpVelocity();
                _verticalVelocity = _jumpVelocity;

                _playerHorizontalMoveHandler.AddVelocity(new(0, _verticalVelocity, 0));
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputJumpSignal>(OnJumpCalled);
        }
    }
}
