using CM.Input.Configs;
using CM.RaycastResolver;
using UnityEngine;

namespace CM.Input
{
    public class MonsterInput : IInput
    {
        private const int PlayerLayer = 1 << 3;
        
        private IHavePosition _target;
        private MovementResolver _movementResolver;
        private TargetResolver _targetResolver;
        private InputConfig _inputConfig;

        private Vector2 _cachedDirection;
        
        public MonsterInputState InputState { get; private set; }
        public bool IsPursuit { get; set; }
        public IHavePosition Target => _target;
        
        public MonsterInput(IHavePosition target, InputConfig config, Transform entityTransform)
        {
            _target = target;
            _inputConfig = config;
            
            _movementResolver = new MovementResolver();
            _movementResolver.Init(_inputConfig.movementMonsterLookDistance, entityTransform);

            _targetResolver = new TargetResolver();
            _targetResolver.Init(_inputConfig.playerCheckDistance, entityTransform);
        }

        public void Init()
        {
            InputState = MonsterInputState.DummyWalk;
        }

        public void Update()
        {
            InputState = MonsterInputState.DummyWalk;
            // if (IsPursuit)
            // {
            //     InputState = MonsterInputState.RunToPlayer;
            //     return;
            // }
            //
            // var lookResult = _targetResolver.GetStatus(_target.Position, PlayerLayer);
            //
            // switch (lookResult)
            // {
            //     case MovementSearchTargetStatus.TargetOutOfBounds:
            //         MovementDirection = _cachedDirection;
            //         InputState = MonsterInputState.DummyWalk;
            //         break;
            //     case MovementSearchTargetStatus.TargetNotSee:
            //         InputState = MonsterInputState.GoToPlayerPosition;
            //         break;
            //     case MovementSearchTargetStatus.TargetSeen:
            //         InputState = MonsterInputState.RunToPlayer;
            //         break;
            //     default:
            //         Debug.LogWarning($"[{GetType().Name}] Unhandled behaviour of input!");
            //         break;
            // }
        }

        public void UpdateDirectionMove()
        {
            var directions = _movementResolver.GetAvailableDirections();

            var direction = directions[Random.Range(0, directions.Count)];

            switch (direction)
            {
                
                case MovementResolverDirections.Forward:
                    _cachedDirection = Vector2.up;
                    break;
                case MovementResolverDirections.Backward:
                    _cachedDirection = Vector2.down;
                    break;
                case MovementResolverDirections.Left:
                    _cachedDirection = Vector2.left;
                    break;
                case MovementResolverDirections.Right:
                    _cachedDirection = Vector2.right;
                    break;
                
                case MovementResolverDirections.None:
                default:
                    Debug.LogWarning($"[{GetType().Name}] Direction incorrect or none!");
                    break;
            }
        }

        public void ResetInput()
        {
            InputState = MonsterInputState.DummyWalk;
            IsPursuit = false;
        }
    }
}