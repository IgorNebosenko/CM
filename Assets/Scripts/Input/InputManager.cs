using System;
using CM.Core.Management;

namespace CM.Input
{
    public class InputManager : IFixedUpdateManager
    {
        public event Action<float> InputTick; 

        public void Simulate(float deltaTime)
        {
            InputTick?.Invoke(deltaTime);
        }
        
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}