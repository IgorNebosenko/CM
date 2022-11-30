using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Projectors
{
    public class ProjectorController : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private Projector projector;

        private IMemoryPool _memoryPool;
        
        private float _lifeTime = -1;
        private float _lifeTimePassed;

        private bool _isStarted;
        private bool _isProcessFade;
        private float _fadeTime;

        public event Action ProjectileDisplayFinished;

        private void Update()
        {
            if (!_isStarted || _lifeTime > 0 || !_isProcessFade)
            {
                _lifeTimePassed += Time.deltaTime;
                if (_lifeTimePassed >= _lifeTime)
                {
                    _isProcessFade = true;
                    BeforeDespawn();
                }
            }
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }
        
        public void OnDespawned()
        {
            _memoryPool = null;
            _isStarted = false;
            _lifeTime = -1f;
            _lifeTimePassed = 0f;
            _isProcessFade = false;
            _fadeTime = 0f;
        }

        public void Dispose()
        {
            _memoryPool?.Despawn(this);
        }

        public void StartProcess()
        {
            _isStarted = true;
        }

        private void BeforeDespawn()
        {
            projector.material.DOFade(0f, _fadeTime).OnComplete(() => ProjectileDisplayFinished?.Invoke());
        }

        public void SetMaterial(Material material)
        {
            projector.material = material;
        }

        public void SetLifeTime(float lifeTime)
        {
            _lifeTime = lifeTime;
        }

        public void SetFadeTime(float fadeTime)
        {
            _fadeTime = Mathf.Clamp(fadeTime, 0, fadeTime);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        private void OnDestroy()
        {
            projector = null;
            _memoryPool = null;

            ProjectileDisplayFinished = null;
        }

        public Material GetMaterialReference()
        {
            return projector.material;
        }

        public class Factory : PlaceholderFactory<ProjectorController>
        {
        }
    }
}