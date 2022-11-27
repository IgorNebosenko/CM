using System;
using CM.Core.Management;
using CM.Entities;
using CM.Maze;

namespace CM.Core.Managers
{
    public class GameManager : IUpdateManager
    {
        private MazeManager _mazeManager;
        private EntityFactory _entityFactory;

        public event Action GameStarted;
        
        public float GameTimePassed { get; private set; }

        public GameManager(MazeManager mazeManager, EntityFactory entityFactory)
        {
            _mazeManager = mazeManager;
            _entityFactory = entityFactory;
            
            StartGame();
        }

        public void Simulate(float deltaTime)
        {
            GameTimePassed += deltaTime;
        }
        
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        private void StartGame()
        {
            GameStarted?.Invoke();
            GameTimePassed = 0f;

            _entityFactory.GetPlayerEntity(_mazeManager.GetPlayerSpawnPoint(), _mazeManager.GetPlayerRotation());
            _entityFactory.GetMonsterEntity(_mazeManager.GetMonsterSpawnPoint());
        }
    }
}