using CM.Input.Configs;
using CM.RaycastResolver;
using UnityEngine;

namespace CM.Input
{
    public class MonsterInput : IInput
    {
        private MonsterInputState _inputState;

        private IHavePosition _target;
        private MovementResolver _movementResolver;
        private TargetResolver _targetResolver;
        private InputConfig _inputConfig;
        
        public Vector2 MovementDirection { get; private set; }
        
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
            _inputState = MonsterInputState.DummyWalk;
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void ResetInput()
        {
            MovementDirection = Vector2.zero;
            _inputState = MonsterInputState.DummyWalk;
        }
    }
}