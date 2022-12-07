using CM.Core.Configs;
using CM.Core.Game;
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
        private bool _isPausedMoving;

        public MazeManager(GameConfig gameConfig, MazeController mazeController, GameManager gameManager)
        {
            _gameConfig = gameConfig;
            _mazeController = mazeController;
            _gameManager = gameManager;
            
            SubscribeEvents();
        }


        public void Simulate(float deltaTime)
        {
            if (_isPausedMoving)
                return;
            
            _mazeController.DoMoveRoof(_gameConfig.speedRoofDecreaseInSecond, deltaTime);
            if (_mazeController.IsRoofHeightDangerous && !_lastDangerousPositionState)
            {
                _lastDangerousPositionState = true;
                _gameManager.OnLowRoofHeight();
            }
            if (_mazeController.IsRoofLessMinimalLevel)
                _gameManager.OnGameEnd(GameTerminationReason.PlayerKilledByRoof);
        }
        
        public void Destroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _gameManager.GameStarted += OnGameStarted;
            _gameManager.GameEnded += OnGameEnded;
        }

        private void UnsubscribeEvents()
        {
            _gameManager.GameStarted -= OnGameStarted;
            _gameManager.GameEnded -= OnGameEnded;
        }

        private void OnGameStarted()
        {
            _mazeController.ResetController();
            _lastDangerousPositionState = false;
            _isPausedMoving = false;
        }

        private void OnGameEnded(GameTerminationReason reason)
        {
            _isPausedMoving = true;
        }
    }
}