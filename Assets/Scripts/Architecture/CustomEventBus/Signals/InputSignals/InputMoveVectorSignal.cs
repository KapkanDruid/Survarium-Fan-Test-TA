using UnityEngine;

namespace Assets.Scripts.Architecture.CustomEventBus
{
    public class InputMoveVectorSignal
    {
        private Vector2 _moveVector;
        public Vector2 MoveVector => _moveVector;

        public InputMoveVectorSignal(Vector2 moveVector)
        {
            _moveVector = moveVector;
        }

        public void SetValue(Vector2 value)
        {
            _moveVector = value;
        }
    }
}