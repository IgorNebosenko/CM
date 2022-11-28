using CM.Entities.Motors;
using CM.Input;
using UnityEngine;

namespace CM.Entities
{
    public class PlayerEntity : MonoBehaviour, IEntity, IHavePosition
    {
        [SerializeField] private float lookXSensitivity = 45;
        [SerializeField] private float lookYSensitivity = 30;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera _camera;

        private PlayerInput _playerInput;
        private EntityData _entityData;

        private MovementMotor _movementMotor;
        
        public Vector3 Position => transform.position;

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            _playerInput.Update();
            _movementMotor.Simulate(deltaTime, _playerInput.MovementDirection, _playerInput.MovementViewDirectionX);
            
            _camera.transform.Rotate(Vector3.left * _playerInput.MovementViewDirectionY * lookYSensitivity * deltaTime);
        }

        public void Init(EntityData data, IInput input)
        {
            _playerInput = (PlayerInput)input;
            _entityData = data;

            _movementMotor = new MovementMotor(characterController, transform, _entityData, lookXSensitivity);
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