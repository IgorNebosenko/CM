using System;
using CM.Core.Management;

namespace CM.Input
{
    public class InputManager : IFixedUpdateManager
    {
        public event Action<float> SimulateInput;

        private GameControls _controls;
        private GameControls.PlayerActions _playerActions;

        public InputManager()
        {
            _controls = new GameControls();
            _playerActions = new GameControls.PlayerActions();
            
            _controls.Enable();
        }

        public void Simulate(float deltaTime)
        {
            SimulateInput?.Invoke(deltaTime);
        }
        
        public void Destroy()
        {
            _controls.Disable();
        }
    }
}