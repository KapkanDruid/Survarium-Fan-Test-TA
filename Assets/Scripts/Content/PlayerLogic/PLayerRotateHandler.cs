using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PLayerRotateHandler : ITickable
    {
        private PlayerData _playerData;

        private Transform _playerTransform;
        private CinemachinePanTilt _panTilt;

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
