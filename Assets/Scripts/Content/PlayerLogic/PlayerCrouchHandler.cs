using Assets.Scripts.Architecture.CustomEventBus;
using Assets.Scripts.Content.GizmosDrawing;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerCrouchHandler : System.IDisposable, IGizmosDrawerOnSelected
    {
        private PlayerData _playerData;
        private CharacterController _characterController;
        private Transform _playerTransform;
        private EventBus _eventBus;

        private bool _isCrouching;
        private float _originalHeight;
        private float _crouchHeight;
        private Vector3 _originalSize;
        private Vector3 _crouchSize;

        public bool IsCrouching => _isCrouching;
        public float CrouchSpeed => _playerData.CrouchSpeed;

        public PlayerCrouchHandler(PlayerData playerData, CharacterController characterController, EventBus eventBus)
        {
            _playerData = playerData;
            _characterController = characterController;
            _eventBus = eventBus;
            _playerTransform = _playerData.PlayerTransform;

            _originalHeight = _characterController.height;
            _originalSize = _playerTransform.localScale;

            _eventBus.Subscribe<InputCrouchSignal>(OnInputCrouchActivate);
        }

        private void OnInputCrouchActivate(InputCrouchSignal signal)
        {
            _crouchHeight = _playerData.CrouchHeight;
            _crouchSize = _originalSize;
            _crouchSize.y = _originalSize.y / (_originalHeight / _crouchHeight);

            if (!_isCrouching)
                Crouch();
            else
                Stand();
        }

        private void Crouch()
        {
            if (_isCrouching)
                return;

            _isCrouching = true;
            _characterController.height = _crouchHeight;
            _playerTransform.localScale = _crouchSize;
        }

        public void Stand()
        {
            if (!_isCrouching)
                return;

            if (!Physics.Raycast(_playerTransform.position, Vector3.up, out RaycastHit hitInfo, _originalHeight - _crouchHeight))
            {
                _isCrouching = false;
                _characterController.height = _originalHeight;
                _playerTransform.localScale = _originalSize;

            }
        }

        public void OnDrawGizmosSelected()
        {
            if (_playerTransform == null)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_playerTransform.position, _playerTransform.position + Vector3.up * _crouchHeight);
            Gizmos.DrawSphere(_playerTransform.position + Vector3.up * _crouchHeight, 0.03f);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputCrouchSignal>(OnInputCrouchActivate);
        }
    }
}
