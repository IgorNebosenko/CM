using CM.Core.Configs;
using CM.Core.Managers;
using CM.Maze;

namespace CM.Core.Management
{
    public class MazeManager : IFixedUpdateManager
    {
        private GameConfig _gameConfig;
        private MazeController _mazeController;
        private GameManager _gameManager;

        private bool _lastDangerousPositionState;

        public MazeManager(GameConfig gameConfig, MazeController mazeController, GameManager gameManager)
        {
            _gameConfig = gameConfig;
            _mazeController = mazeController;
            _gameManager = gameManager;
        }


        public void Simulate(float deltaTime)
        {
            _mazeController.DoMoveRoof(_gameConfig.speedRoofDecreaseInSecond, deltaTime);
            if (_mazeController.IsRoofHeightDangerous && !_lastDangerousPositionState)
            {
                _lastDangerousPositionState = true;
            }
        }
        
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            _mazeController.ResetController();
            _lastDangerousPositionState = false;
        }
    }
}