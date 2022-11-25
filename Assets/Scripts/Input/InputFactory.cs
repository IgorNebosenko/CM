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
            return new PlayerInput(_gameControls);
        }

        public IInput CreateMonsterInput(IHavePosition playerPosition)
        {
            throw new NotImplementedException();
        }
    }
}