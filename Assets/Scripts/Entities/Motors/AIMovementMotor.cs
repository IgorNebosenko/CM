using CM.Input;
using CM.Input.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace CM.Entities.Motors
{
    public class AIMovementMotor
    {
        private CharacterController _characterController;
        private Transform _entityTransform;
        private EntityData _entityData;
        private InputConfig _inputConfig;
        private MonsterInput _monsterInput;
        private NavMeshAgent _agent;

        private float _timeMovePassed;
        private float _timePursuitPassed;

        public AIMovementMotor(CharacterController characterController, Transform entityTransform, EntityData entityData,
            InputConfig inputConfig, MonsterInput monsterInput, NavMeshAgent agent)
        {
            _characterController = characterController;
            _entityTransform = entityTransform;
            _entityData = entityData;
            _inputConfig = inputConfig;
            _monsterInput = monsterInput;
            _agent = agent;
        }

        public void Simulate(float deltaTime)
        {
            
        }
    }
}