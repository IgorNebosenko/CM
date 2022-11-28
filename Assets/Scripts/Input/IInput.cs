using UnityEngine;

namespace CM.Input
{
    public interface IInput
    {
        Vector3 MovementDirection { get; }
        
        void Init();
        void Update();
        void ResetInput();
    }
}