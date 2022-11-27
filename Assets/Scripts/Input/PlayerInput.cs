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
        public float MovementViewDirectionX { get; private set; }
        public float MovementViewDirectionY { get; private set; }

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
                MovementDirection = _movementDirectionCached;
            else
            {
                var direction = _gameControls.Player.Movement.ReadValue<Vector2>();
                MovementDirection = Vector3.right * direction.x + Vector3.forward * direction.y;
                MovementDirection *= deltaTime;
            }

            if (!_isMovementViewUpdate)
            {
                MovementViewDirectionX = _movementViewDirectionCached.x;
                MovementViewDirectionY = _movementViewDirectionCached.y;
            }
            else
            {
                var direction = new Vector2(
                    _gameControls.Player.LookX.ReadValue<float>(),
                    _gameControls.Player.LookY.ReadValue<float>());
                Debug.Log($"[DEV] {direction}");
                MovementViewDirectionX = direction.x * deltaTime; 
                MovementViewDirectionY = direction.y * deltaTime;
            }
        }

        public void ResetInput()
        {
            MovementDirection = Vector2.zero;
            MovementViewDirectionX = 0;
            MovementViewDirectionY = 0;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _isMovementUpdate = true;
        }

        public void OnLookX(InputAction.CallbackContext context)
        {
            _isMovementViewUpdate = true;
        }

        public void OnLookY(InputAction.CallbackContext context)
        {
            _isMovementViewUpdate = true;
        }
    }
}