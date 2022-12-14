using UnityEngine;

namespace CM.RaycastResolver
{
    public interface IRaycastResolver
    {
        void Init(float lookDistance, Transform transform);
    }
}