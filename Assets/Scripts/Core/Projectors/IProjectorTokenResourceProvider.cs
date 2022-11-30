using System.Threading.Tasks;
using UnityEngine;

namespace ElectrumGames.Core.Projectors
{
    public interface IProjectorTokenResourceProvider
    {
        Task<T> GetAddressableAsync<T>(string path) where T : Object;
    }
}