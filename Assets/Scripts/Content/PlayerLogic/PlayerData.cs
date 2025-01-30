using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject _characterObject;
        [SerializeField] private CinemachinePanTilt _cinemachinePanTilt;

        public float MoveSpeedOnGround => _playerConfig.MoveSpeedOnGround;
        public float JumpTime => _playerConfig.JumpTime;
        public float JumpHeight => _playerConfig.JumpHeight;
        public float MoveSpeedOnFall => _playerConfig.MoveSpeedOnFall;

        public Transform PlayerTransform => _playerTransform;
        public GameObject CharacterObject => _characterObject;
        public CinemachinePanTilt CinemachinePanTilt => _cinemachinePanTilt;
    }
}
