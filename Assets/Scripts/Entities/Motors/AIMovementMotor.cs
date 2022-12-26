using System.Collections.Generic;
using CM.Input;
using CM.Input.Configs;
using CM.Maze;
using UnityEngine;
using UnityEngine.AI;

namespace CM.Entities.Motors
{
    public class AIMovementMotor
    {
        private NavMeshAgent _agent;
        private Transform _agentTransform;
        private EntityData _entityData;
        private InputConfig _inputConfig;
        private IHavePosition _player;
        private List<Vector3> _movementPositions;

        private Vector3 _targetPosition;
        private float _timePursuitPassed;

        public AIMovementMotor(NavMeshAgent agent, EntityData entityData,
            InputConfig inputConfig, IHavePosition player, MazeController mazeController)
        {
            _agent = agent;
            _agentTransform = _agent.transform;
            _entityData = entityData;
            _inputConfig = inputConfig;
            _player = player;
            _movementPositions = mazeController.GetMonsterMovementPositions();
        }

        public void Init()
        {
            RandomChangeWalkPosition();
        }

        public void Simulate(float deltaTime, MonsterInputState state)
        {
            switch (state)
            {
                case MonsterInputState.DummyWalk:
                    if (Vector3.Distance(_agentTransform.position, _targetPosition) <=
                        _inputConfig.minimalDistanceToWalkTarget)
                        RandomChangeWalkPosition();
                    break;
            }
        }

        private void RandomChangeWalkPosition()
        {
            MoveTo(_movementPositions[Random.Range(0, _movementPositions.Count)], false);
        }

        private void MoveTo(Vector3 targetPosition, bool isPursuit)
        {
            _targetPosition = targetPosition;
            _agent.destination = targetPosition;
            
            _agent.speed = _entityData.speed;
            if (isPursuit)
                _agent.speed *= _inputConfig.pursuitBoostSpeed;
        }
    }
}