using UnityEngine;

namespace CM.Entities.Motors
{
    public class MovementMotor
    {
        private CharacterController _characterController;
        private Transform _entityTransform;
        private EntityData _entityData;
        private float _lookXSensitivity;

        public MovementMotor(CharacterController characterController, Transform entityTransform, 
            EntityData entityData, float lookXSensitivity)
        {
            _characterController = characterController;
            _entityTransform = entityTransform;
            _entityData = entityData;
            _lookXSensitivity = lookXSensitivity;
        }

        public void Simulate(float deltaTime, Vector3 inputDirection, float additionalAngleLook)
        {
            inputDirection *= deltaTime;
            
            var movementDirection = _entityTransform.right * inputDirection.x +
                                    _entityTransform.forward * inputDirection.z;
            
            _characterController.Move(movementDirection * _entityData.speed);
            _entityTransform.Rotate(Vector3.up * additionalAngleLook * _lookXSensitivity * deltaTime);
        }
    }
}