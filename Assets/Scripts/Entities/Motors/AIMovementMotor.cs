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
        private NavMeshAgent _agent;
        private IHavePosition _target;

        private float _timeMovePassed;
        private float _timePursuitPassed;

        private Vector3? _targetPosition;

        public AIMovementMotor(CharacterController characterController, Transform entityTransform, EntityData entityData,
            InputConfig inputConfig, NavMeshAgent agent, IHavePosition target)
        {
            _characterController = characterController;
            _entityTransform = entityTransform;
            _entityData = entityData;
            _inputConfig = inputConfig;
            _agent = agent;
            _target = target;
        }

        public AiMotorCallback Simulate(float deltaTime, Vector2 inputDirection, MonsterInputState state)
        {
            switch (state)
            {
                case MonsterInputState.DummyWalk:
                    _timeMovePassed += deltaTime;

                    if (_targetPosition != null)
                    {
                        _targetPosition = null;
                        _agent.isStopped = true;
                    }

                    var velocity = (_entityTransform.right * inputDirection.x +
                                    _entityTransform.forward * inputDirection.y) *
                                   _entityData.speed;
                    _characterController.Move(velocity * deltaTime);
                    
                    if (_timeMovePassed >= _inputConfig.walkDuration)
                    {
                        _timeMovePassed -= _inputConfig.walkDuration;
                        return AiMotorCallback.UpdateMoveDirection;
                    }
                    return AiMotorCallback.Nothing;
                case MonsterInputState.GoToPlayerPosition:
                    if (_targetPosition == null)
                        ActivateAgent();

                    if (Vector3.Distance(_targetPosition.Value, _entityTransform.position) < 0.1f)
                        return AiMotorCallback.EndMoveToPlayer;
                    return AiMotorCallback.Nothing;
                case MonsterInputState.RunToPlayer:
                    _timePursuitPassed += deltaTime;

                    if (_targetPosition == null)
                        ActivateAgent(_inputConfig.pursuitBoostSpeed);
                    else
                        _agent.destination = _target.Position;

                    if (_timePursuitPassed >= _inputConfig.pursuitDuration)
                    {
                        _timePursuitPassed = 0f;
                        return AiMotorCallback.EndPursuitPlayer;
                    }
                    return AiMotorCallback.EndPursuitPlayer;
                default:
                    Debug.LogWarning($"[{GetType().Name}] Unhandled state variant!");
                    return AiMotorCallback.Nothing;
            }
        }

        private void ActivateAgent(float speedModifier = 1f)
        {
            _agent.isStopped = false;
            _targetPosition = _target.Position;
            _agent.speed = _entityData.speed * speedModifier;
            _agent.destination = _targetPosition.Value;
        }
    }
}