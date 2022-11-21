using CM.Input;
using UnityEngine;

namespace CM.Entities
{
    public class PlayerEntity : MonoBehaviour, IEntity, IHavePosition
    {
        public Vector3 Position => transform.position;
        
        public void Init(EntityData data, IInput input)
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