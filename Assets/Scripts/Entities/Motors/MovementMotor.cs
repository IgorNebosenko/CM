using DG.Tweening;
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

        public void Simulate(float deltaTime, Vector2 inputDirection, float additionalAngleLook)
        {
            var velocity = (_entityTransform.right * inputDirection.x + 
                            _entityTransform.forward * inputDirection.y) *
                           _entityData.speed;
            _characterController.Move(velocity * deltaTime);
            _entityTransform.DORotate(
                Vector3.up * additionalAngleLook * _lookXSensitivity * deltaTime + _entityTransform.eulerAngles,
                deltaTime);
        }
    }
}