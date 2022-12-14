using UnityEngine;

namespace CM.Input
{
    public class MonsterInput : IInput
    {
        private MonsterInputState _inputState;
        
        public Vector2 MovementDirection { get; private set; }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void ResetInput()
        {
            MovementDirection = Vector2.zero;
        }
    }
}