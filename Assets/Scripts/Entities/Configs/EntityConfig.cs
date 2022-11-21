using UnityEngine;

namespace CM.Entities.Configs
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "Configs/Entity Config")]
    public class EntityConfig : ScriptableObject
    {
        [SerializeField] private PlayerEntity playerEntity;
        [SerializeField] private EntityData playerExtraData;

        [SerializeField] private MonsterEntity monsterEntity;
        [SerializeField] private EntityData monsterExtraData;

        public (PlayerEntity playerEntity, EntityData data) GetPlayerData()
        {
            return (playerEntity, playerExtraData);
        }

        public (MonsterEntity monsterEntity, EntityData data) GetMonsterData()
        {
            return (monsterEntity, monsterExtraData);
        }
    }
}