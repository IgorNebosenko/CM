using System;
using CM.Input.Configs;
using UnityEngine;

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

        public MonsterInput CreateMonsterInput(IHavePosition playerPosition, InputConfig config, Transform entityTransform)
        {
            var input = new MonsterInput(playerPosition, config, entityTransform);
            input.Init();
            return input;
        }
    }
}