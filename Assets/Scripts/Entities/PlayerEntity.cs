using System;
using CM.Input;
using UnityEngine;

namespace CM.Entities
{
    public class PlayerEntity : MonoBehaviour, IEntity, IHavePosition
    {
        [SerializeField] private CharacterController characterController;

        private PlayerInput _playerInput;
        private EntityData _entityData;
        
        public Vector3 Position => transform.position;

        private void Update()
        {
            _playerInput.Simulate(Time.deltaTime);
            
            characterController.Move(_playerInput.MovementDirection * _entityData.speed);
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