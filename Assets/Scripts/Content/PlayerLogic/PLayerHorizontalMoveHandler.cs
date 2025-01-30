using Assets.Scripts.Architecture.CustomEventBus;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PLayerHorizontalMoveHandler : ITickable, System.IDisposable
    public class PlayerHorizontalMoveHandler : ITickable, System.IDisposable
    {
        private EventBus _eventBus;
        private PlayerData _playerData;

        private Transform _playerTransform;
        private CharacterController _characterController;

        private Vector3 _movementVector;
        private Vector3 _lateMoveVector;
        private float _yVelocity;

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
            var movementSpeed = _playerData.MoveSpeed;

            _movementVector = _playerTransform.right * _inputVector.x + _playerTransform.forward * _inputVector.y;
            _movementVector *= movementSpeed;

            _movementVector.y = _yVelocity;

            _characterController.Move(_movementVector * Time.deltaTime);
        }

        public void SetYVelocity(float value)
        {
            _yVelocity = value;
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
