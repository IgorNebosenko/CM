using System;
using System.Collections.Generic;
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

        public List<MovementResolverDirections> GetAvailableDirections()
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
            
            var result = new List<MovementResolverDirections>();

            for (var i = 0; i < distances.Length; i++)
            {
                if (Math.Abs(distances[i].distance - FreeDirectionFlag) < ToleranceDifference)
                    result.Add(distances[i].direction);
            }

            if (result.Count != 0)
                return result;

            (MovementResolverDirections direction, float distance) largestVal = (MovementResolverDirections.None, 0f);
            
            for (var i = 0; i < distances.Length; i++)
            {
                if (distances[i].distance > largestVal.distance)
                    largestVal = distances[i];
            }
            
            result.Add(largestVal.direction);

            return result;
        }

        private float GetDistance(Vector3 direction)
        {
            if (Physics.Raycast(_transform.position, direction, out var forwardInfo, _lookDistance))
                return forwardInfo.distance;
            return FreeDirectionFlag;
        }
    }
}
