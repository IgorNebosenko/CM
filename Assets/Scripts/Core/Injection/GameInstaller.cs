using CM.Maze.Configs;
using CM.Core.Management;
using CM.Core.Managers;
using CM.Entities.Configs;
using CM.Input;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private MazeSegmentsConfig segmentData;
        [SerializeField] private EntityConfig entityConfig;

        public override void InstallBindings()
        {
            Container.Bind<MazeSegmentsConfig>().FromInstance(segmentData).AsSingle(); //for different segments
            Container.Bind<EntityConfig>().FromInstance(entityConfig).AsSingle();

            Container.Bind(typeof(IManagerProvider), typeof(IManagersRunner)).To<ManagersRunner>().AsSingle();

            Container.Bind<GameControls>().AsSingle();
            
            BindManagerExplicit<GameManager>();
            BindManagerExplicit<InputManager>();
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