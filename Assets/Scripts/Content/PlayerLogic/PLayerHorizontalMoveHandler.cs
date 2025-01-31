using Assets.Scripts.Architecture.CustomEventBus;
using Assets.Scripts.Content.GizmosDrawing;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerHorizontalMoveHandler : ITickable, System.IDisposable, IGizmosDrawer
    {
        private EventBus _eventBus;
        private PlayerData _playerData;

        private Transform _playerTransform;
        private CharacterController _characterController;

        private Vector3 _movementVector;
        private Vector3 _lateMoveVector;
        private Vector3 _inputMoveVector;
        private Vector3 _additionalVelocity;
        private bool _isRunning;

        public PlayerHorizontalMoveHandler(PlayerData playerData, EventBus eventBus, CharacterController characterController)
        {
            _playerData = playerData;
            _eventBus = eventBus;
            _characterController = characterController;

            _playerTransform = _playerData.PlayerTransform;

            _eventBus.Subscribe<InputMoveVectorSignal>(ChangeInputMoveVector);
            _eventBus.Subscribe<InputRunSignal>(OnInputRunActivate);
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

            _movementVector += _additionalVelocity;
            _additionalVelocity = Vector3.zero;

            _characterController.Move(_movementVector * Time.deltaTime);
        }

        private void DetermineGroundMovement()
        {
            float movementSpeed;
            if (_isRunning)
                movementSpeed = _playerData.RunSpeed;
            else
                movementSpeed = _playerData.MoveSpeedOnGround;

            _movementVector = _playerTransform.right * _inputMoveVector.x + _playerTransform.forward * _inputMoveVector.y;

            _movementVector *= movementSpeed;

            _lateMoveVector = _movementVector;
            _lateMoveVector.y = 0;
        }

        private void DetermineFallMovement()
        {
            var movementSpeed = _playerData.MoveSpeedOnFall;

            _movementVector = _playerTransform.right * _inputMoveVector.x + _playerTransform.forward * _inputMoveVector.y;

            _movementVector *= movementSpeed;

            _movementVector += _lateMoveVector;
        }

        public void AddVelocity(Vector3 vector)
        {
            _additionalVelocity += vector;
        }

        private void ChangeInputMoveVector(InputMoveVectorSignal signal)
        {
            _inputMoveVector = signal.InputVector;
        }

        private void OnInputRunActivate(InputRunSignal signal)
        {
            if (!_isRunning)
                _isRunning = true;
            else
                _isRunning = false;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputMoveVectorSignal>(ChangeInputMoveVector);
            _eventBus.Unsubscribe<InputRunSignal>(OnInputRunActivate);
        }

        public void OnDrawGizmos()
        {
            if (_playerTransform == null)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_playerTransform.position, _movementVector);
            Gizmos.DrawSphere(_playerTransform.position + _movementVector, 0.1f);
        }
    }
}
