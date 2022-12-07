using UnityEngine;

namespace CM.Core.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public float speedRoofDecreaseInSecond = 0.1f;
        public float timeMonsterFollowPlayer = 3f;
        public float timeBeforeDeath = 2.5f;
    }
}