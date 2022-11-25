using UnityEngine;
using UnityEngine.InputSystem;

namespace CM.Input
{
    public class PlayerInput : IInput, GameControls.IPlayerActions
    {
        private GameControls _gameControls;

        private bool _isMovementUpdate = true;
        private bool _isMovementViewUpdate = true;

        private Vector2 _movementDirectionCached;
        private Vector2 _movementViewDirectionCached;
        
        public Vector2 MovementDirection { get; private set; }
        public Vector2 MovementViewDirection { get; private set; }

        public PlayerInput(GameControls gameControls)
        {
            _gameControls = gameControls;
        }

        public void Init()
        {
            _gameControls.Player.SetCallbacks(this);
            _gameControls.Enable();
        }

        public void Simulate(float deltaTime)
        {
            if (_isMovementUpdate)
            {
                MovementDirection = _gameControls.Player.Movement.ReadValue<Vector2>();
                _isMovementUpdate = false;
            }
            else
                MovementDirection = _movementDirectionCached;

            if (_isMovementViewUpdate)
            {
                MovementViewDirection = _gameControls.Player.Movement.ReadValue<Vector2>();
                _isMovementViewUpdate = false;
            }
            else
                MovementViewDirection = _movementViewDirectionCached;
        }

        public void ResetInput()
        {
            MovementDirection = Vector2.zero;
            MovementViewDirection = Vector2.zero;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _isMovementUpdate = true;
        }

        public void OnViewMovement(InputAction.CallbackContext context)
        {
            _isMovementViewUpdate = true;
        }
    }
}