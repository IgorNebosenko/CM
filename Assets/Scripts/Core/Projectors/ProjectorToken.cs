using System;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Projectors
{
    public class ProjectorToken : IPoolable<string, Vector3, Quaternion, IMemoryPool>, IDisposable
    {
        private ProjectorController _projectorController;
        private IMemoryPool _memoryPool;

        private Vector3 _position;
        private Quaternion _rotation;

        [Inject] 
        private ProjectorController.Factory _factory;

        private IProjectorTokenResourceProvider _provider;

        private string _materialPath;

        private bool _isNeedUnload;

        public void OnSpawned(string path, Vector3 position, Quaternion rotation, IMemoryPool pool)
        {
            _memoryPool = pool;
            _projectorController = _factory.Create();
            _projectorController.ProjectileDisplayFinished += OnProjectileDisplayFinished;
            
            _materialPath = path;
            _position = position;
            _rotation = rotation;
        }
        
        public void OnDespawned()
        {
            _memoryPool = null;
            _isNeedUnload = false;
            _position = Vector3.zero;
            _rotation = Quaternion.identity;

            _projectorController.ProjectileDisplayFinished -= OnProjectileDisplayFinished;
            _projectorController.Dispose();
        }

        public void Dispose()
        {
            _isNeedUnload = true;
            _memoryPool?.Despawn(this);
        }

        private void OnProjectileDisplayFinished()
        {
            Dispose();
        }

        private async void LoadAndShowAsync()
        {
            _materialPath = _materialPath.ToLower();

            var material = await _provider.GetAddressableAsync<Material>(_materialPath);
            if (material == null || _projectorController == null)
            {
                Debug.LogError($"[{GetType().Name}] Can't load material with path {_materialPath}. Projector entity not showed... material = {material}, controller = {_projectorController}");
            }
            else
            {
                _projectorController.SetMaterial(material);
                ApplyProjectorSettings();
                _projectorController.StartProcess();
            }
        }

        public ProjectorToken SetParent(Transform parent)
        {
            _projectorController.transform.SetParent(parent);
            return this;
        }

        public ProjectorToken SetLifeTime(float lifeTime)
        {
            _projectorController.SetLifeTime(lifeTime);
            return this;
        }

        public ProjectorToken SetFadeTime(float fadeTime)
        {
            _projectorController.SetLifeTime(fadeTime);
            return this;
        }

        public ProjectorToken SetUnload(bool state)
        {
            _isNeedUnload = state;
            return this;
        }

        private void ApplyProjectorSettings()
        {
            _projectorController.SetPosition(_position);
            _projectorController.SetRotation(_rotation);
        }

        public void Show()
        {
            if (string.IsNullOrEmpty(_materialPath))
            {
                Debug.LogError($"[{GetType().Name}] Empty path to material!");
                return;
            }
            
            LoadAndShowAsync();
        }

        private void SetResourceProvider(IProjectorTokenResourceProvider provider)
        {
            _provider = provider;
        }

        public class Factory : PlaceholderFactory<string, Vector3, Quaternion, ProjectorToken>
        {
            [Inject]
            private IProjectorTokenResourceProvider _provider;

            public override ProjectorToken Create(string materialPath, Vector3 position, Quaternion rotation)
            {
                var token = base.Create(materialPath, position, rotation);
                token.SetResourceProvider(_provider);
                return token;
            }
        }
    }
}