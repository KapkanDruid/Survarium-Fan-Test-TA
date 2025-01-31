using Assets.Scripts.Content.GizmosDrawing;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerSlideHandler : ITickable, IGizmosDrawerOnSelected
    {
        private readonly PlayerData _playerData;
        private readonly Transform _playerTransform;
        private readonly CharacterController _characterController;
        private readonly PlayerHorizontalMoveHandler _moveHandler;

        private Vector3 _slideVector;
        private float _rayLength;

        public PlayerSlideHandler(PlayerData playerData, CharacterController characterController, PlayerHorizontalMoveHandler moveHandler)
        {
            _playerData = playerData;
            _moveHandler = moveHandler;
            _characterController = characterController;

            _playerTransform = _playerData.PlayerTransform;
        }

        public void Tick()
        {
            if (_characterController.isGrounded)
                DetermineSlideVector();

            _moveHandler.AddVelocity(_slideVector);

            _slideVector = Vector3.zero;
        }

        private void DetermineSlideVector()
        {
            _rayLength = _characterController.height / 2 + _playerData.GroundRayLength;

            if (Physics.Raycast(_playerTransform.position, Vector3.down, out RaycastHit hit, _rayLength))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);

                if (angle > _playerData.SlideAngle)
                {
                    _slideVector = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;

                    var baseSlideSpeed = _playerData.SlideSpeed;

                    var modifiedSlideSpeed = _playerData.SlideSpeed + (angle - _playerData.SlideAngle) * _playerData.SlideSpeedMultiplier;

                    modifiedSlideSpeed = Mathf.Max(modifiedSlideSpeed, baseSlideSpeed);

                    _slideVector *= modifiedSlideSpeed;
                }
            }
        }

        public void OnDrawGizmosSelected()
        {
            if (_playerTransform == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_playerTransform.position, _playerTransform.position + Vector3.down * _rayLength);
            Gizmos.DrawSphere(_playerTransform.position + Vector3.down * _rayLength, 0.03f);
        }
    }
}
