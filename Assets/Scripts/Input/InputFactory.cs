using System;

namespace CM.Input
{
    public class InputFactory
    {
        private InputManager _inputManager;
        
        public InputFactory(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
        
        public IInput CreatePlayerInput()
        {
            throw new NotImplementedException();
        }

        public IInput CreateMonsterInput(IHavePosition playerPosition)
        {
            throw new NotImplementedException();
        }
    }
}