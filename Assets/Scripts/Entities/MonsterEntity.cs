using CM.Input;
using CM.Input.Configs;
using UnityEngine;

namespace CM.Entities
{
    public class MonsterEntity : MonoBehaviour, IEntity
    {
        public void Init(EntityData data, IInput input, InputConfig inputConfig)
        {
            throw new System.NotImplementedException();
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