using System;
using CM.Core.Management;
using UnityEngine;

namespace CM.Input
{
    public class InputManager : IFixedUpdateManager
    {
        public event Action<float> SimulateInput;

        public void Simulate(float deltaTime)
        {
            SimulateInput?.Invoke(deltaTime);
        }
        
        public void Destroy()
        {
        }
    }
}