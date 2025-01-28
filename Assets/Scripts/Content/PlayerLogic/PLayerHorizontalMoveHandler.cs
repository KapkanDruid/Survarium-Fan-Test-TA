using Assets.Scripts.Architecture.CustomEventBus;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PLayerHorizontalMoveHandler : ITickable, System.IDisposable
    {
        private EventBus _eventBus;
        private PlayerData _playerData;

        private Transform _playerTransform;
        private CharacterController _characterController;

        private Vector3 _movementVector;
        private Vector3 _inputVector;
        private float _yVelocity;

        public PLayerHorizontalMoveHandler(PlayerData playerData, EventBus eventBus, CharacterController characterController)
        {
            _playerData = playerData;
            _eventBus = eventBus;
            _characterController = characterController;

            _playerTransform = _playerData.PlayerTransform;

            _eventBus.Subscribe<InputMoveVectorSignal>(ChangeInputVector);
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

        private void ChangeInputVector(InputMoveVectorSignal signal)
        {
            _inputVector = signal.MoveVector;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputMoveVectorSignal>(ChangeInputVector);
        }
    }
}
