using UnityEngine;

namespace Assets.Scripts.Architecture.CustomEventBus
{
    public class InputMoveVectorSignal : InputVectorSignal
    {
        public InputMoveVectorSignal(Vector2 inputVector) : base(inputVector) { }
    }
}