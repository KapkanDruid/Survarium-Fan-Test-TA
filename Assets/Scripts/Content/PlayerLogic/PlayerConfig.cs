using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [CreateAssetMenu(menuName = "Survarium/Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeedOnGround;
        [SerializeField] private float _moveSpeedOnFall;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _jumpHeight;

        public float MoveSpeedOnGround => _moveSpeedOnGround;
        public float JumpTime => _jumpTime;
        public float JumpHeight => _jumpHeight;
        public float MoveSpeedOnFall => _moveSpeedOnFall;
    }
}
