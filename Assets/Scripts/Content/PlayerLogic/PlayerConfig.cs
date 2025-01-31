using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [CreateAssetMenu(menuName = "Survarium/Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeedOnGround;
        [SerializeField] private float _moveSpeedOnFall;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _crouchHeight;
        [SerializeField] private float _crouchSpeed;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _slideAngle;
        [SerializeField] private float _slideSpeed;
        [SerializeField] private float _slideSpeedMultiplierByAngle;

        public float MoveSpeedOnGround => _moveSpeedOnGround;
        public float JumpTime => _jumpTime;
        public float JumpHeight => _jumpHeight;
        public float MoveSpeedOnFall => _moveSpeedOnFall;
        public float SlideAngle => _slideAngle;
        public float SlideSpeed => _slideSpeed;
        public float SlideSpeedMultiplierByAngle => _slideSpeedMultiplierByAngle;
        public float RunSpeed => _runSpeed;
        public float CrouchHeight => _crouchHeight;
        public float CrouchSpeed => _crouchSpeed;
    }
}
