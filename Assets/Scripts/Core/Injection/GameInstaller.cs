using CM.Configs;
using CM.Core.Managers;
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

            Container.Bind(typeof(IManagerProvider), typeof(IManagersRunner)).To<ManagersRunner>().AsSingle();
        }


        private ConcreteIdArgConditionCopyNonLazyBinder BindManagerImplicit<T>() where T : IManager
        {
            return Container.Bind<IManager>().To<T>().AsSingle();
        }
        
        private ConcreteIdArgConditionCopyNonLazyBinder BindManagerExplicit<T>() where T : IManager
        {
            var binder = Container.Bind<T>().AsSingle();
            Container.Bind<IManager>().To<T>().FromResolve();
            return binder;
        }
    }
}