using CM.Entities.Motors;
using CM.GameObjects.Dynamic;
using CM.GameObjects.Visual;
using CM.Input;
using CM.Input.Configs;
using CM.Maze;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CM.Entities
{
    public class MonsterEntity : MonoBehaviour, IEntity, IHavePosition
    {
        [SerializeField] private NavMeshAgent agent;
        
        private DiContainer _container;
        private MonsterInput _monsterInput;
        private EntityData _entityData;
        private InputConfig _inputConfig;
        private MazeController _mazeController;

        private AIMovementMotor _movementMotor;

        private IEntityVisual[] _entityVisuals;
        
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _entityVisuals = new IEntityVisual[]
            {
                new MonsterSoundsHandler()
            };
        }

        private void Start()
        {
            for (var i = 0; i < _entityVisuals.Length; i++)
                _entityVisuals[i].Init(_container, this);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            var cachedPosition = transform.position;

            _monsterInput.Update();
            _movementMotor.Simulate(deltaTime,
                _monsterInput.InputState);

            if (transform.position != cachedPosition)
            {
                for (var i = 0; i < _entityVisuals.Length; i++)
                    _entityVisuals[i].OnIterateStep(deltaTime);
            }
        }

        public void Init(DiContainer container, EntityData data, IInput input)
        {
            _container = container;
            _monsterInput = (MonsterInput) input;
            _entityData = data;
            _inputConfig = _container.Resolve<InputConfig>();
            _mazeController = _container.Resolve<MazeController>();

            _movementMotor = new AIMovementMotor(agent, _entityData,
                _inputConfig, _monsterInput.Target, _mazeController);
            _movementMotor.Init();
        }

        public void DoDeath()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}