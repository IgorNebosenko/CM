using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CM.GameObjects.Dynamic
{
    public class MonsterSoundsHandler : EntitySoundsHandlerBase
    {
        private bool _isRun;

        protected override float FootstepDuration
        {
            get
            {
                if (_isRun)
                    return soundsConfig.monsterRunFootstepDuration;
                return soundsConfig.monsterWalkFootstepDuration;
            }
        }
        
        protected override AssetReference GetRandomFootstepReference()
        {
            if (soundsConfig.monsterFootstepsSounds == null || soundsConfig.monsterFootstepsSounds.Length == 0)
            {
                Debug.Log($"[{GetType().Name}] footstep list is empty!");
                return null;
            }

            return soundsConfig.monsterFootstepsSounds[Random.Range(0, soundsConfig.monsterFootstepsSounds.Length)];
        }
    }
}