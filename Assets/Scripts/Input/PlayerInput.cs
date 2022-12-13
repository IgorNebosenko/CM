using UnityEngine;
using UnityEngine.InputSystem;

#if !UNITY_EDITOR && UNITY_ANDROID
using UnityEngine.EventSystems;
#endif

namespace CM.Input
{
    public class PlayerInput : IInput, GameControls.IPlayerActions
    {
        private GameControls _gameControls;

        private bool _isMovementUpdate = true;
        private bool _isMovementViewUpdate = true;
        private bool _isTouchUpdated = true;

        private Vector2 _movementDirectionCached;
        private Vector2 _movementViewDirectionCached;
        
        public Vector2 MovementDirection { get; private set; }
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

        public void Update()
        {
            if (!_isMovementUpdate)
                MovementDirection = _movementDirectionCached;
            else
            {
                MovementDirection = _gameControls.Player.Movement.ReadValue<Vector2>();
            }

            MovementViewDirectionX = _movementViewDirectionCached.x;
            MovementViewDirectionY = _movementViewDirectionCached.y;
            
#if UNITY_EDITOR
            if (!_isMovementViewUpdate) 
                return;
            
            var direction = new Vector2(
                _gameControls.Player.LookX.ReadValue<float>(),
                _gameControls.Player.LookY.ReadValue<float>());
            
            MovementViewDirectionX = direction.x; 
            MovementViewDirectionY = direction.y;
#elif UNITY_ANDROID

            var eventSystem = EventSystem.current;
            var touches = Touchscreen.current.touches;
            
            if (eventSystem.IsPointerOverGameObject(touches[0].touchId.ReadValue()))
            {
                if (touches.Count > 1 && touches[1].isInProgress)
                {
                    if (eventSystem.IsPointerOverGameObject(touches[1].touchId.ReadValue()))
                        return;

                    var  touchDeltaPosition = Touchscreen.current.touches[1].delta.ReadValue();
                    
                    MovementViewDirectionX = touchDeltaPosition.x;
                    MovementViewDirectionY = touchDeltaPosition.y;
                }
            }
            else
            {
                if (touches.Count > 0 && touches[0].isInProgress)
                {
                    if (eventSystem.IsPointerOverGameObject(touches[0].touchId.ReadValue()))
                        return;
                    

                    var touchDeltaPosition = touches[0].delta.ReadValue();
                    
                    MovementViewDirectionX = touchDeltaPosition.x;
                    MovementViewDirectionY = touchDeltaPosition.y;
                }

            }
#endif
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
            
            OnLook(context);
        }

        public void OnLookY(InputAction.CallbackContext context)
        {
            OnLook(context);
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            _isMovementViewUpdate = true;
        }
    }
}