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
        
        public Vector3 Position => transform.position;

        private void Update()
        {
            _playerInput.Simulate(Time.deltaTime);

            var movementDirection = transform.right * _playerInput.MovementDirection.x +
                                    transform.forward * _playerInput.MovementDirection.z;
            
            characterController.Move(movementDirection * _entityData.speed);
            transform.Rotate(Vector3.up * _playerInput.MovementViewDirectionX * lookXSensitivity);
            _camera.transform.Rotate(Vector3.left * _playerInput.MovementViewDirectionY * lookYSensitivity);
        }

        public void Init(EntityData data, IInput input)
        {
            _playerInput = (PlayerInput)input;
            _entityData = data;
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