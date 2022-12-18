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
        private MonsterInput _monsterInput;
        private EntityData _entityData;
        private InputConfig _inputConfig;

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
            var deltaTime = Time.deltaTime;
            var cachedPosition = transform.position;

            _monsterInput.Update();
            var motorCallback = _movementMotor.Simulate(deltaTime, _monsterInput.MovementDirection,
                _monsterInput.InputState);

            switch (motorCallback)
            {
                case AiMotorCallback.Nothing:
                    break;
                case AiMotorCallback.UpdateMoveDirection:
                    _monsterInput.UpdateDirectionMove();
                    break;
                case AiMotorCallback.PursuitProcess:
                    _monsterInput.IsPursuit = true;
                    break;
                case AiMotorCallback.EndMoveToPlayer:
                    _monsterInput.IsPursuit = false;
                    break;
            }

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

            _movementMotor = new AIMovementMotor(characterController, transform, _entityData, 
                _inputConfig, agent, _monsterInput.Target);
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