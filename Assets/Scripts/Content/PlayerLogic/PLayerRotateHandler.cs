using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PLayerRotateHandler : ITickable
    {
        private readonly PlayerData _playerData;

        private readonly Transform _playerTransform;
        private readonly CinemachinePanTilt _panTilt;

        public PLayerRotateHandler(PlayerData playerData)
        {
            _playerData = playerData;

            _playerTransform = _playerData.PlayerTransform;
            _panTilt = _playerData.CinemachinePanTilt;
        }

        public void Tick()
        {
            Rotate();
        }

        private void Rotate()
        {
            _playerTransform.rotation = Quaternion.Euler(0, _panTilt.PanAxis.Value, 0);
        }
    }
}
