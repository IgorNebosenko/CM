using CM.Input;
using UnityEngine;
using Zenject;

namespace CM.Entities
{
    public class MonsterEntity : MonoBehaviour, IEntity
    {
        public void Init(DiContainer container, EntityData data, IInput input)
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