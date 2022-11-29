using System;
using CM.Entities.Configs;
using CM.Input;
using CM.Input.Configs;
using UnityEngine;

namespace CM.Entities
{
    public class EntityFactory : IDisposable
    {
        private InputFactory _inputFactory;
        private EntityConfig _entityConfig;
        private InputConfig _inputConfig;

        private Transform _root;

        private IHavePosition _playerPosition;
        
        public EntityFactory(InputFactory inputFactory, EntityConfig entityConfig, InputConfig inputConfig)
        {
            _inputFactory = inputFactory;
            _entityConfig = entityConfig;
            _inputConfig = inputConfig;

            _root = GameObject.Instantiate(new GameObject()).transform;
            _root.gameObject.name = "Entities";
            _root.position = Vector3.zero;
        }

        public PlayerEntity GetPlayerEntity(Vector3 startPosition, Quaternion startRotation)
        {
            var playerData = _entityConfig.GetPlayerData();
            
            var entity = GameObject.Instantiate(playerData.playerEntity, startPosition, startRotation, _root);
            entity.Init(playerData.data, _inputFactory.CreatePlayerInput(), _inputConfig);
            _playerPosition = entity;

            return entity;
        }

        public MonsterEntity GetMonsterEntity(Vector3 startPosition)
        {
            var monsterData = _entityConfig.GetMonsterData();

            var entity = GameObject.Instantiate(monsterData.monsterEntity, startPosition, Quaternion.identity, _root);
            entity.Init(monsterData.data, _inputFactory.CreateMonsterInput(_playerPosition), _inputConfig);

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