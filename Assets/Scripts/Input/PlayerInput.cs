using UnityEngine;
using UnityEngine.InputSystem;

namespace CM.Input
{
    public class PlayerInput : IInput, GameControls.IPlayerActions
    {
        private GameControls _gameControls;

        private bool _isMovementUpdate = true;
        private bool _isMovementViewUpdate = true;

        private Vector3 _movementDirectionCached;
        private Vector3 _movementViewDirectionCached;
        
        public Vector3 MovementDirection { get; private set; }
        public Vector3 MovementViewDirection { get; private set; }

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
            if (!_isMovementUpdate)
            {
                MovementDirection = _movementDirectionCached;
                return;
            }

            var direction = _gameControls.Player.Movement.ReadValue<Vector2>();
            MovementDirection = Vector3.right * direction.x + Vector3.forward * direction.y;
            MovementDirection *= deltaTime;
            

            if (!_isMovementViewUpdate)
            {
            }
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