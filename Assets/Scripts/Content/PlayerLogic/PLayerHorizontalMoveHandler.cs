using Assets.Scripts.Architecture.CustomEventBus;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerHorizontalMoveHandler : ITickable, System.IDisposable
    {
        private EventBus _eventBus;
        private PlayerData _playerData;

        private Transform _playerTransform;
        private CharacterController _characterController;

        private Vector3 _movementVector;
        private Vector3 _lateMoveVector;
        private Vector3 _inputMoveVector;
        private Vector3 _additionalVelocity;

        public PlayerHorizontalMoveHandler(PlayerData playerData, EventBus eventBus, CharacterController characterController)
        {
            _playerData = playerData;
            _eventBus = eventBus;
            _characterController = characterController;

            _playerTransform = _playerData.PlayerTransform;

            _eventBus.Subscribe<InputMoveVectorSignal>(ChangeInputMoveVector);
        }

        public void Tick()
        {
            Move();
        }

        private void Move()
        {
            if (_characterController.isGrounded)
                DetermineGroundMovement();
            else
                DetermineFallMovement();

            _characterController.Move(_movementVector * Time.deltaTime);
        }

        private void DetermineGroundMovement()
        {
            var movementSpeed = _playerData.MoveSpeedOnGround;

            _movementVector = _playerTransform.right * _inputMoveVector.x + _playerTransform.forward * _inputMoveVector.y;

            _movementVector *= movementSpeed;

            _lateMoveVector = _movementVector;
            _lateMoveVector.y = 0;

            _movementVector += _additionalVelocity;
        }

        private void DetermineFallMovement()
        {
            var movementSpeed = _playerData.MoveSpeedOnFall;

            _movementVector = _playerTransform.right * _inputMoveVector.x + _playerTransform.forward * _inputMoveVector.y;

            _movementVector *= movementSpeed;

            _movementVector += _lateMoveVector;

            _movementVector += _additionalVelocity;
        }

        public void AddVelocity(Vector3 vector)
        {
            _additionalVelocity = vector;
        }

        private void ChangeInputMoveVector(InputMoveVectorSignal signal)
        {
            _inputMoveVector = signal.InputVector;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputMoveVectorSignal>(ChangeInputMoveVector);
        }
    }
}
