using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private float _groundRayLength;
        [SerializeField] private PlayerConfig _playerConfig;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject _characterObject;
        [SerializeField] private CinemachinePanTilt _cinemachinePanTilt;
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        public float MoveSpeedOnGround => _playerConfig.MoveSpeedOnGround;
        public float JumpTime => _playerConfig.JumpTime;
        public float JumpHeight => _playerConfig.JumpHeight;
        public float MoveSpeedOnFall => _playerConfig.MoveSpeedOnFall;
        public float GroundRayLength => _groundRayLength;
        public float SlideAngle => _playerConfig.SlideAngle;
        public float SlideSpeed => _playerConfig.SlideSpeed;
        public float SlideSpeedMultiplier => _playerConfig.SlideSpeedMultiplierByAngle;
        public float RunSpeed => _playerConfig.RunSpeed;
        public float CrouchHeight => _playerConfig.CrouchHeight;
        public float CrouchSpeed => _playerConfig.CrouchSpeed;
        public float FOVIncreasePercent => _playerConfig.FOVIncreasePercent;
        public float FOVIncreaseDuration => _playerConfig.FOVIncreaseDuration;

        public Transform PlayerTransform => _playerTransform;
        public GameObject CharacterObject => _characterObject;
        public CinemachinePanTilt CinemachinePanTilt => _cinemachinePanTilt;
        public CinemachineCamera CinemachineCamera => _cinemachineCamera;
    }
}
