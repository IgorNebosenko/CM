using UnityEngine;

namespace CM.RaycastResolver
{
    public class TargetResolver : IRaycastResolver
    {
        private float _lookDistance;
        private Transform _transform;
        
        public void Init(float lookDistance, Transform transform)
        {
            _lookDistance = lookDistance;
            _transform = transform;
        }

        public MovementSearchTargetStatus GetStatus(Vector3 targetPosition, int layerToSearch)
        {
            Debug.LogWarning($"[{GetType().Name}] Need check it!");
            if (Vector3.Distance(_transform.position, targetPosition) > _lookDistance)
                return MovementSearchTargetStatus.TargetOutOfBounds;

            if (Physics.Raycast(_transform.position, (_transform.position - targetPosition).normalized, _lookDistance,
                    layerToSearch))
                return MovementSearchTargetStatus.TargetSeen;
            return MovementSearchTargetStatus.TargetNotSee;
        }
    }
}