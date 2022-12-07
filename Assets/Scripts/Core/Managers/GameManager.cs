using System;
using CM.Core.Game;
using CM.Core.Management;
using CM.Entities;
using CM.Maze;
using UnityEngine;

namespace CM.Core.Managers
{
    public class GameManager : IUpdateManager
    {
        private MazeController _mazeController;
        private EntityFactory _entityFactory;

        public event Action GameStarted;
        public event Action LowRoofHeight;
        public event Action MonsterSeenPlayer;
        public event Action<GameTerminationReason> GameEnded;

        public float GameTimePassed { get; private set; }

        public GameManager(MazeController mazeController, EntityFactory entityFactory)
        {
            _mazeController = mazeController;
            _entityFactory = entityFactory;
            
            OnGameStart();
        }

        public void Simulate(float deltaTime)
        {
            GameTimePassed += deltaTime;
        }
        
        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnGameStart()
        {
            GameStarted?.Invoke();
            GameTimePassed = 0f;

            _entityFactory.GetPlayerEntity(_mazeController.GetPlayerSpawnPoint(), Quaternion.identity);
            //_entityFactory.GetMonsterEntity(_mazeController.GetMonsterSpawnPoint());
        }

        public void OnLowRoofHeight()
        {
            LowRoofHeight?.Invoke();
        }

        public void OnMonsterSeePlayer()
        {
            MonsterSeenPlayer?.Invoke();
        }

        public void OnGameEnd(GameTerminationReason reason)
        {
            GameEnded?.Invoke(reason);
        }
    }
}