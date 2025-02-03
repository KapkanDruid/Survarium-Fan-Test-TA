using Assets.Scripts.Architecture.CustomEventBus;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class CameraEffectsHandler : System.IDisposable
    {
        private readonly PlayerHorizontalMoveHandler _moveHandler;
        private readonly CinemachineCamera _cinemachineCamera;
        private readonly PlayerData _playerData;
        private readonly EventBus _eventBus;

        private readonly CancellationToken _cancellationToken;

        private float _defaultFOV;
        private bool _isActive;
        private float _currentFOV;

        public CameraEffectsHandler(PlayerData playerData, EventBus eventBus, PlayerHorizontalMoveHandler moveHandler)
        {
            _playerData = playerData;
            _cinemachineCamera = _playerData.CinemachineCamera;
            _eventBus = eventBus;
            _moveHandler = moveHandler;

            _cancellationToken = _playerData.CharacterObject.GetCancellationTokenOnDestroy();

            _eventBus.Subscribe<InputRunSignal>(OnInputRunActivate);
        }

        private void OnInputRunActivate(InputRunSignal signal)
        {
            if (!_isActive)
                FOVKick();
            else
                SetDefault();
        }

        private void FOVKick()
        {
            if (_isActive)
                return;

            _defaultFOV = _cinemachineCamera.Lens.FieldOfView;

            _isActive = true;

            IncreaseFOV().Forget();
        }

        private async UniTask IncreaseFOV()
        {
            float elapsedTime = 0f;

            var newFOV = _defaultFOV + _defaultFOV * (_playerData.FOVIncreasePercent / 100);

            while (elapsedTime < _playerData.FOVIncreaseDuration && _isActive)
            {
                try
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken);

                }
                catch (OperationCanceledException)
                {
                    return;
                }

                if (_moveHandler.MovementVector.x == 0 || _moveHandler.MovementVector.z == 0)
                    continue;

                _currentFOV = Mathf.Lerp(_defaultFOV, newFOV, elapsedTime / _playerData.FOVIncreaseDuration);

                _cinemachineCamera.Lens.FieldOfView = _currentFOV;

                elapsedTime += Time.deltaTime;
            }
        }

        private void SetDefault()
        {
            if (!_isActive)
                return;

            _cinemachineCamera.Lens.FieldOfView = _defaultFOV;

            _isActive = false;

            DecreaseFOV().Forget();
        }

        private async UniTask DecreaseFOV()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _playerData.FOVIncreaseDuration / 2 && !_isActive)
            {
                try
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken);

                }
                catch (OperationCanceledException)
                {
                    return;
                }

                var currentFOV = Mathf.Lerp(_currentFOV, _defaultFOV, elapsedTime / (_playerData.FOVIncreaseDuration / 2));

                _cinemachineCamera.Lens.FieldOfView = currentFOV;

                elapsedTime += Time.deltaTime;
            }

            _currentFOV = _defaultFOV;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<InputRunSignal>(OnInputRunActivate);
        }
    }
}
