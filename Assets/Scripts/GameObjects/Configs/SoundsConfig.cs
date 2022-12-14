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
        [Space]
        public AssetReference entityFootstepsPreset;
        [Space]
        public AssetReference[] playerFootstepsSounds;
        public float playerFootstepDuration = 0.6f;
        [Space]
        public AssetReference monsterSeenPlayerEffect;
        public AssetReference[] monsterFootstepsSounds;
        public float monsterWalkFootstepDuration = 0.75f;
        public float monsterRunFootstepDuration = 0.4f;

    }
}