using System;
using UnityEngine;

namespace CM.RaycastResolver
{
    public class MovementResolver : IRaycastResolver
    {
        private const float FreeDirectionFlag = -1f;
        private const float ToleranceDifference = 0.1f;
        
        private float _lookDistance;
        private Transform _transform;
        
        public void Init(float lookDistance, Transform transform)
        {
            _lookDistance = lookDistance;
            _transform = transform;
        }

        public MovementResolverDirections GetAvailableDirections()
        {
            var forward = _transform.forward;
            var right = _transform.right;
            
            var distances = new (MovementResolverDirections direction, float distance)[]
            {
                (MovementResolverDirections.Forward, GetDistance(forward)),
                (MovementResolverDirections.Backward, GetDistance(forward * -1f)),
                (MovementResolverDirections.Right, GetDistance(right)),
                (MovementResolverDirections.Left, GetDistance(right * -1f))
            };
            
            var hasAvailableDirection = false;
            var result = MovementResolverDirections.None;

            for (var i = 0; i < distances.Length; i++)
            {
                if (!(Math.Abs(distances[i].distance - FreeDirectionFlag) < ToleranceDifference)) 
                    continue;
                
                hasAvailableDirection = true;
                result += (int)distances[i].direction;
            }

            if (hasAvailableDirection)
                return result;

            (MovementResolverDirections direction, float distance) largestVal = (MovementResolverDirections.None, 0f);
            
            for (var i = 0; i < distances.Length; i++)
            {
                if (distances[i].distance > largestVal.distance)
                    largestVal = distances[i];
            }

            return largestVal.direction;
        }

        private float GetDistance(Vector3 direction)
        {
            if (Physics.Raycast(_transform.position, direction, out var forwardInfo, _lookDistance))
                return forwardInfo.distance;
            return FreeDirectionFlag;
        }
    }
}
