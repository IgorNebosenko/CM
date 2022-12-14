using UnityEngine;

namespace CM.Input.Configs
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Configs/Input Config")]
    public class InputConfig : ScriptableObject
    {
        public float lookXSensitivity = 3;
        public float lookYSensitivity = 3;
        public float lookYAngleClamp = 85f;
        [Space] 
        public float movementMonsterLookDistance = 3f;
        public float playerCheckDistance = 10f;
        public float pursuitDuration = 3f;
    }
}