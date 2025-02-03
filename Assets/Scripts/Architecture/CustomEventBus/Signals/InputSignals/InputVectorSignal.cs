using UnityEngine;

namespace Assets.Scripts.Architecture.CustomEventBus
{
    public abstract class InputVectorSignal
    {
        private Vector2 _inputVector;
        public Vector2 InputVector => _inputVector;

        public InputVectorSignal(Vector2 inputVector)
        {
            _inputVector = inputVector;
        }

        public void SetValue(Vector2 value)
        {
            _inputVector = value;
        }
    }
}