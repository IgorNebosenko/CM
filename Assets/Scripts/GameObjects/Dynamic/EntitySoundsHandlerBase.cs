using CM.GameObjects.Configs;
using CM.GameObjects.Visual;
using CM.Input;
using ElectrumGames.Core.Audio;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CM.GameObjects.Dynamic
{
    public abstract class EntitySoundsHandlerBase : IEntityVisual
    {
        private IHavePosition _target;
        
        private GameAudioTokenProvider _audioTokenProvider;
        protected SoundsConfig soundsConfig;

        private float _timePassed;

        protected abstract float FootstepDuration { get; }
        
        public void Init(DiContainer container, IHavePosition target)
        {
            _target = target;
            
            soundsConfig = container.Resolve<SoundsConfig>();
            _audioTokenProvider = container.Resolve<GameAudioTokenProvider>();
        }

        public void OnIterateStep(float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > FootstepDuration)
            {
                _timePassed = 0f;
                PlayFootstep();
            }
        }

        private void PlayFootstep()
        {
            var stepReference = GetRandomFootstepReference();

            var token = _audioTokenProvider.GetToken((string)stepReference.RuntimeKey,
                (string)soundsConfig.entityFootstepsPreset.RuntimeKey);
            
            token.SetPosition(_target.Position).Play();
        }

        protected abstract AssetReference GetRandomFootstepReference();
    }
}