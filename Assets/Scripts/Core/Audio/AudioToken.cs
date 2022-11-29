using System;
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
            }
        }
    }
}