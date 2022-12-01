using System;
using CM.Entities;
using CM.GameObjects.Configs;
using ElectrumGames.Core.Audio;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Random = UnityEngine.Random;

namespace CM.GameObjects.Dynamic
{
    public class PlayerSoundsHandler : MonoBehaviour
    {
        [SerializeField] private PlayerEntity entity;

        private SoundsConfig _soundsConfig;
        private GameAudioTokenProvider _audioTokenProvider;
        
        private float _timePassed;

        [Inject]
        private void Construct(SoundsConfig soundsConfig, GameAudioTokenProvider audioTokenProvider)
        {
            _soundsConfig = soundsConfig;
            _audioTokenProvider = audioTokenProvider;
        }

        private void OnEnable()
        {
            entity.StepIterate += OnStepIterate;
        }

        private void OnDisable()
        {
            entity.StepIterate -= OnStepIterate;
        }

        private void OnStepIterate(float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > _soundsConfig.playerFootstepDuration)
            {
                _timePassed = 0f;
                PlayFootstep();
            }
        }

        private void PlayFootstep()
        {
            var stepReference = GetRandomFootstepReference();

            var token = _audioTokenProvider.GetTokenFromGuid(stepReference.AssetGUID,
                _soundsConfig.playerFootstepsPreset.AssetGUID);
            
            token.SetPosition(transform.position).Play();
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