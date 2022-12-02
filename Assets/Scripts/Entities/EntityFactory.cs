using System;
using CM.Entities.Configs;
using CM.Input;
using CM.Input.Configs;
using UnityEngine;
using Zenject;

namespace CM.Entities
{
    public class EntityFactory : IDisposable
    {
        private DiContainer _container;
        private InputFactory _inputFactory;
        private EntityConfig _entityConfig;
        private InputConfig _inputConfig;

        private Transform _root;

        private IHavePosition _playerPosition;
        
        public EntityFactory(DiContainer container)
        {
            _container = container;
            _inputFactory = _container.Resolve<InputFactory>();
            _entityConfig = _container.Resolve<EntityConfig>();
            _inputConfig = _container.Resolve<InputConfig>();

            _root = GameObject.Instantiate(new GameObject()).transform;
            _root.gameObject.name = "Entities";
            _root.position = Vector3.zero;
        }

        public PlayerEntity GetPlayerEntity(Vector3 startPosition, Quaternion startRotation)
        {
            var playerData = _entityConfig.GetPlayerData();
            
            var entity = GameObject.Instantiate(playerData.playerEntity, startPosition, startRotation, _root);
            entity.Init(_container, playerData.data, _inputFactory.CreatePlayerInput());
            _playerPosition = entity;

            return entity;
        }

        public MonsterEntity GetMonsterEntity(Vector3 startPosition)
        {
            var monsterData = _entityConfig.GetMonsterData();

            var entity = GameObject.Instantiate(monsterData.monsterEntity, startPosition, Quaternion.identity, _root);
            entity.Init(_container, monsterData.data, _inputFactory.CreateMonsterInput(_playerPosition));

            return entity;
        }


        public void Dispose()
        {
            foreach (GameObject child in _root)
            {
                if (child.TryGetComponent<IEntity>(out var entity))
                    entity.Dispose();
                
                GameObject.Destroy(child);
            }
            
            GameObject.Destroy(_root);
        }
    }
}