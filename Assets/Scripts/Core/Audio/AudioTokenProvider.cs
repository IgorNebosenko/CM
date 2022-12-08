using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Core.Management;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace ElectrumGames.Core.Audio
{
    public class AudioTokenProvider : IAudioTokenResourceProvider, IManager
    {
        private ConcurrentDictionary<string, Object> _cashedObjectsAsync =
            new ConcurrentDictionary<string, Object>();
        private BlockingCollection<string> _loadingResourcesAsync =
            new BlockingCollection<string>();

        private List<AudioToken> _tokens = new List<AudioToken>();

        public async Task<T> GetAddressableAsync<T>(string path) where T : Object
        {
            if (_cashedObjectsAsync.TryGetValue(path, out var obj))
                return (T) obj;

            if (_loadingResourcesAsync.Contains(path))
            {
                while (_loadingResourcesAsync.Contains(path))
                    await Task.Yield();
                return (T) _cashedObjectsAsync[path];
            }

            _loadingResourcesAsync.TryAdd(path);

            var handle = Addressables.LoadAssetAsync<T>(path);
            await handle.Task;
            var result = handle.Result;
            _cashedObjectsAsync.TryAdd(path, result);
            _loadingResourcesAsync.TryTake(out var outs);
            return result;
        }

        public void Destroy()
        {
            for (var i = 0; i < _tokens.Count; i++)
            {
                if (_tokens[i] == null)
                    continue;
                _tokens[i].Dispose();
            }
            _tokens.Clear();
            
            _cashedObjectsAsync.Clear();
        }
    }
}