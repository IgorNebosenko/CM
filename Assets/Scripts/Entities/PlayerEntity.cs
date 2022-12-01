using System;
using CM.Entities.Motors;
using CM.Input;
using CM.Input.Configs;
using UnityEngine;

namespace CM.Entities
{
    public class PlayerEntity : MonoBehaviour, IEntity, IHavePosition
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera _camera;

        private PlayerInput _playerInput;
        private EntityData _entityData;
        private InputConfig _inputConfig;

        private MovementMotor _movementMotor;
        private CameraMotor _cameraMotor;
        
        public Vector3 Position => transform.position;

        public event Action<float> StepIterate;

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            var cachedPosition = transform.position;
            
            _playerInput.Update();
            _movementMotor.Simulate(deltaTime, _playerInput.MovementDirection, _playerInput.MovementViewDirectionX);
            _cameraMotor.Simulate(deltaTime, _playerInput.MovementViewDirectionY);
            
            if (transform.position != cachedPosition)
                StepIterate?.Invoke(deltaTime);
        }

        public void Init(EntityData data, IInput input, InputConfig inputConfig)
        {
            _playerInput = (PlayerInput)input;
            _entityData = data;
            _inputConfig = inputConfig;

            _movementMotor = new MovementMotor(characterController, transform, _entityData, _inputConfig);
            _cameraMotor = new CameraMotor(_camera.transform, _inputConfig);
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