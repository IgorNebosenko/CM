using System;

namespace CM.Input
{
    public class InputFactory
    {
        private GameControls _gameControls;
        
        public InputFactory(GameControls gameControls)
        {
            _gameControls = gameControls;
        }
        
        public PlayerInput CreatePlayerInput()
        {
            var input = new PlayerInput(_gameControls);
            input.Init();
            return input;
        }

        public IInput CreateMonsterInput(IHavePosition playerPosition)
        {
            throw new NotImplementedException();
        }
    }
}