using CM.Maze.Configs;
using CM.Core.Management;
using CM.Core.Managers;
using CM.Entities;
using CM.Entities.Configs;
using CM.Input;
using CM.Input.Configs;
using CM.Maze;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private MazeSegmentsConfig segmentData;
        [SerializeField] private EntityConfig entityConfig;
        [SerializeField] private InputConfig inputConfig;
        [Space] 
        [SerializeField] private MazeController mazeController;

        public override void InstallBindings()
        {
            Container.Bind<MazeSegmentsConfig>().FromInstance(segmentData).AsSingle(); //for different segments
            Container.Bind<EntityConfig>().FromInstance(entityConfig).AsSingle();
            Container.Bind<InputConfig>().FromInstance(inputConfig).AsSingle();

            Container.Bind(typeof(IManagerProvider), typeof(IManagersRunner)).To<ManagersRunner>().AsSingle();

            Container.Bind<GameControls>().AsSingle();

            Container.Bind<EntityFactory>().AsSingle();
            Container.Bind<InputFactory>().AsSingle();
            
            BindManagerExplicit<GameManager>();
            BindManagerExplicit<MazeManager>();

            Container.Bind<MazeController>().FromInstance(mazeController).WhenInjectedInto<MazeManager>();
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