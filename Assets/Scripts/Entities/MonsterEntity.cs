using CM.GameObjects.Dynamic;
using CM.GameObjects.Visual;
using CM.Input;
using UnityEngine;
using Zenject;

namespace CM.Entities
{
    public class MonsterEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private CharacterController characterController;
        
        private IHavePosition _target;
        private DiContainer _container;
        private EntityData _entityData;
        private IInput _monsterInput;

        private IEntityVisual[] _entityVisuals;

        private void Awake()
        {
            _entityVisuals = new IEntityVisual[]
            {
                new MonsterSoundsHandler()
            };
        }

        public void Init(DiContainer container, EntityData data, IInput input)
        {
            _container = container;
            _entityData = data;
            _monsterInput = input;
            
            //Add some motor for move
        }

        public void DoDeath()
        {
            throw new System.NotImplementedException();
        }

        public void SetTarget(IHavePosition target)
        {
            _target = target;
        }

        public void Dispose()
        {
        }
    }
}