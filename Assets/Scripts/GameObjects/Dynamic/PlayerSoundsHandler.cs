using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CM.GameObjects.Dynamic
{
    public class PlayerSoundsHandler : EntitySoundsHandlerBase
    {
        protected override float FootstepDuration => soundsConfig.playerFootstepDuration;

        protected override AssetReference GetRandomFootstepReference()
        {
            if (soundsConfig.playerFootstepsSounds == null || soundsConfig.playerFootstepsSounds.Length == 0)
            {
                Debug.Log($"[{GetType().Name}] footstep list is empty!");
                return null;
            }

            return soundsConfig.playerFootstepsSounds[Random.Range(0, soundsConfig.playerFootstepsSounds.Length)];
        }
    }
}