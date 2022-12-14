using System;
using CM.Entities.Motors;
using CM.GameObjects.Dynamic;
using CM.GameObjects.Visual;
using CM.Input;
using CM.Input.Configs;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CM.Entities
{
    public class MonsterEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private NavMeshAgent agent;
        
        private DiContainer _container;
        private EntityData _entityData;
        private InputConfig _inputConfig;
        private MonsterInput _monsterInput;

        private AIMovementMotor _movementMotor;

        private IEntityVisual[] _entityVisuals;

        private void Awake()
        {
            _entityVisuals = new IEntityVisual[]
            {
                new MonsterSoundsHandler()
            };
        }

        private void Update()
        {
            _movementMotor.Simulate(Time.deltaTime);
        }

        public void Init(DiContainer container, EntityData data, IInput input)
        {
            _container = container;
            _entityData = data;
            _inputConfig = _container.Resolve<InputConfig>();
            _monsterInput = (MonsterInput) input;

            _movementMotor = new AIMovementMotor(characterController, transform, _entityData, _inputConfig,
                _monsterInput, agent);
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