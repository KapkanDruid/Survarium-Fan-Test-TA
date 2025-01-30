using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private float _moveSpeedOnGround;
        [SerializeField] private float _moveSpeedOnFall;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _jumpHeight;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject _characterObject;
        [SerializeField] private CinemachinePanTilt _cinemachinePanTilt;

        public float MoveSpeedOnGround => _moveSpeedOnGround;
        public float JumpTime => _jumpTime;
        public float JumpHeight => _jumpHeight;
        public float MoveSpeedOnFall => _moveSpeedOnFall;

        public Transform PlayerTransform => _playerTransform;
        public GameObject CharacterObject => _characterObject;
        public CinemachinePanTilt CinemachinePanTilt => _cinemachinePanTilt;
    }
}
