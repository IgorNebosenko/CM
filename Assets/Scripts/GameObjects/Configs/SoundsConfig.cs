using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CM.GameObjects.Configs
{
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "Configs/Sounds Config", order = 0)]
    public class SoundsConfig : ScriptableObject
    {
        public AssetReference globalAudioPreset;
        public AssetReference ambientMusic;
        public AssetReference lowRoofMusic;
        public AssetReference monsterSeenPlayerEffect;
        [Space]
        public AssetReference playerFootstepsPreset;
        public AssetReference[] playerFootstepsSounds;
        public float playerFootstepDuration = 0.6f;
    }
}