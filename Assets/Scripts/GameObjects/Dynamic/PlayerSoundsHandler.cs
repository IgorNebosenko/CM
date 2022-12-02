using CM.GameObjects.Configs;
using CM.GameObjects.Visual;
using CM.Input;
using ElectrumGames.Core.Audio;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CM.GameObjects.Dynamic
{
    public class PlayerSoundsHandler : IEntityVisual
    {
        private IHavePosition _target;

        private SoundsConfig _soundsConfig;
        private GameAudioTokenProvider _audioTokenProvider;
        
        private float _timePassed;
        
        public void Init(DiContainer container, IHavePosition target)
        {
            _target = target;
            
            _soundsConfig = container.Resolve<SoundsConfig>();
            _audioTokenProvider = container.Resolve<GameAudioTokenProvider>();
        }

        public void OnIterateStep(float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > _soundsConfig.playerFootstepDuration)
            {
                _timePassed = 0f;
                PlayFootstep();
            }
        }

        public void Destroy()
        {
            
        }

        private void PlayFootstep()
        {
            var stepReference = GetRandomFootstepReference();

            var token = _audioTokenProvider.GetTokenFromGuid(stepReference.AssetGUID,
                _soundsConfig.playerFootstepsPreset.AssetGUID);
            
            token.SetPosition(_target.Position).Play();
        }

        private AssetReference GetRandomFootstepReference()
        {
            if (_soundsConfig.playerFootstepsSounds == null || _soundsConfig.playerFootstepsSounds.Length == 0)
            {
                Debug.Log($"[{GetType().Name}] footstep list is empty!");
                return null;
            }

            return _soundsConfig.playerFootstepsSounds[Random.Range(0, _soundsConfig.playerFootstepsSounds.Length)];
        }
    }
}