using CM.Configs;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private MazeSegmentsConfig _segmentData;
        
        public override void InstallBindings()
        {
            Container.Bind<MazeSegmentsConfig>().FromInstance(_segmentData).AsSingle();
        }
    }
}