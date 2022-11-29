using System.Threading.Tasks;
using UnityEngine;

namespace ElectrumGames.Core.Audio
{
    public interface IAudioTokenResourceProvider
    {
        Task<T> GetAddressableAsync<T>(string path) where T : Object;
    }
}