using System;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Audio
{
    public class AudioSourceController : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private AudioSource audioSource;

        private IMemoryPool _memoryPool;
        private bool _isClipPlayFinished;

        public event Action ClipPlayFinished;

        private void Update()
        {
            #if UNITY_EDITOR
            transform.name = audioSource.clip == null
                ? "AudioSource"
                : $"AudioSource {audioSource.clip.name}, mute:{audioSource.mute}, loop:{audioSource.loop}";
#endif
            if (audioSource.clip == null)
                return;

            if (!audioSource.isPlaying && !_isClipPlayFinished)
            {
                ClipPlayFinished?.Invoke();
                _isClipPlayFinished = true;
            }
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }
        
        public void OnDespawned()
        {
            _memoryPool = null;
            audioSource.loop = false;
            audioSource.mute = false;
            audioSource.clip = null;
            _isClipPlayFinished = false;
        }

        public void Dispose()
        {
            _memoryPool?.Despawn(this);
        }

        public AudioSourceController SetClip(AudioClip clip)
        {
            audioSource.clip = clip;
            return this;
        }

        public AudioSourceController SetPreset(AudioSource presetInstance)
        {
            presetInstance.SetPreset(presetInstance);
            return this;
        }

        public AudioSourceController Loop(bool state)
        {
            audioSource.loop = state;
            return this;
        }

        public AudioSourceController Mute(bool state)
        {
            audioSource.mute = state;
            return this;
        }

        private void Play()
        {
            audioSource.Play();
            _isClipPlayFinished = false;
        }

        private void OnDestroy()
        {
            audioSource = null;
            _memoryPool = null;

            ClipPlayFinished = null;
        }

        public AudioClip GetClipReference()
        {
            return audioSource.clip;
        }

        public class Factory : PlaceholderFactory<AudioSourceController>
        {
        }
    }
}