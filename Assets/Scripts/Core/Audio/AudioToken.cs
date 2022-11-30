using System;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Audio
{
    public class AudioToken : IPoolable<string, string, IMemoryPool>, IDisposable
    {
        private const string DefaultAudioPreset = "audio/defaultaudiopreset";
        
        private AudioSourceController _audioSourceController;
        private IMemoryPool _memoryPool;

        [Inject] 
        private AudioSourceController.Factory _factory;

        private IAudioTokenResourceProvider _provider;

        private string _soundPath;
        private string _presetPath;

        private bool _isLoop;
        private bool _isMute;
        private bool _isNeedUnload;

        public bool IsMute => _isMute;

        public void OnSpawned(string soundPath, string persetPath, IMemoryPool pool)
        {
            _memoryPool = pool;
            _audioSourceController = _factory.Create();
            _audioSourceController.ClipPlayFinished += OnClipPlayFinished;

            _soundPath = soundPath;
            _presetPath = persetPath;
        }
        
        public void OnDespawned()
        {
            if (_isNeedUnload)
                _audioSourceController.SetClip(null);

            _memoryPool = null;
            _isLoop = false;
            _isMute = false;
            _isNeedUnload = false;

            _audioSourceController.ClipPlayFinished -= OnClipPlayFinished;
            _audioSourceController.Dispose();
        }

        public void Dispose()
        {
            _isNeedUnload = true;
            _memoryPool?.Despawn(this);
        }

        private void OnClipPlayFinished()
        {
            Dispose();
        }

        private async void LoadAndPlayAsync()
        {
            if (!string.IsNullOrEmpty(_presetPath))
            {
                _presetPath = _presetPath.ToLower();
                var handle = await _provider.GetAddressableAsync<GameObject>(_presetPath);

                if (handle == null)
                {
                    Debug.LogWarning($"[{GetType().Name}] Can't loaf preset with path = {_presetPath}. Preset set as default = {DefaultAudioPreset}");
                    handle = await _provider.GetAddressableAsync<GameObject>(DefaultAudioPreset);

                    if (handle == null)
                        Debug.LogWarning($"[{GetType().Name}] Can't load default audio preset!");
                    else
                        _audioSourceController.SetPreset(handle.GetComponent<AudioSource>());
                }
                else if (_audioSourceController != null)
                {
                    _audioSourceController.SetPreset(handle.GetComponent<AudioSource>());
                }
            }

            _soundPath = _soundPath.ToLower();
            var audioClip = await _provider.GetAddressableAsync<AudioClip>(_soundPath);

            if (audioClip == null || _audioSourceController == null)
            {
                Debug.LogError($"[{GetType().Name}] Can't load audio clip with path {_soundPath}. Audio will not played... clip = {audioClip}, controller = {_audioSourceController}");
            }
            else
            {
                _audioSourceController.SetClip(audioClip);
                ApplyAudioSourceSettings();
                _audioSourceController.Play();
            }
        }
        
        private void ApplyAudioSourceSettings()
        {
            _audioSourceController.Loop(_isLoop);
            _audioSourceController.Mute(_isMute);
        }

        public AudioToken SetParent(Transform parent)
        {
            _audioSourceController.transform.SetParent(parent);
            return this;
        }

        public AudioToken SetPosition(Vector3 position)
        {
            _audioSourceController.transform.position = position;
            return this;
        }

        public AudioToken SetUnload(bool state)
        {
            _isNeedUnload = state;
            return this;
        }

        public AudioToken Loop()
        {
            _isLoop = true;
            _audioSourceController.Loop(_isLoop);
            return this;
        }

        public AudioToken Mute(bool state)
        {
            _isMute = state;
            _audioSourceController.Mute(_isMute);
            return this;
        }

        public void Play()
        {
            if (string.IsNullOrEmpty(_soundPath))
            {
                Debug.LogError($"[{GetType().Name}] Empty path to audio clip!");
                return;
            }
            
            LoadAndPlayAsync();
        }

        private void SetResourceProvider(IAudioTokenResourceProvider provider)
        {
            _provider = provider;
        }

        public class Factory : PlaceholderFactory<string, string, AudioToken>
        {
            [Inject] 
            private IAudioTokenResourceProvider _provider;

            public override AudioToken Create(string soundPath, string presetPath)
            {
                var token = base.Create(soundPath, presetPath);
                token.SetResourceProvider(_provider);
                return token;
            }
        }
    }
}