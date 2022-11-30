using System;
using System.Collections.Generic;
using System.Reflection;
using CM.UI;
using ElectrumGames.Core.Audio;
using ElectrumGames.Core.Projectors;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.MVP.Utils;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private CanvasContainer canvasContainerTemplate;
        
        public override void InstallBindings()
        {
            var canvasContainer = Instantiate(canvasContainerTemplate);
            
            var presenterFactory = new PresenterFactory(Container);
            var allViews = AutoViewInstall();

            var viewManager = new ViewManager(allViews.views, canvasContainer.viewContainer, presenterFactory);
            var popupManager = new PopupManager(allViews.popups, canvasContainer.popupContainer, presenterFactory);

            Container.Bind<PresenterFactory>().FromInstance(presenterFactory).AsSingle();
            Container.BindInstance(viewManager);
            Container.BindInstance(popupManager);

            Container.BindFactory<AudioSourceController, AudioSourceController.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(25)
                    .FromComponentInNewPrefabResource("Audio/AudioSource")
                    .UnderTransformGroup("AudioTokenSourcesPool"));

            Container.BindFactory<ProjectorController, ProjectorController.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(25)
                    .FromComponentInNewPrefabResource("Projectors/ProjectorSource")
                    .UnderTransformGroup("ProjectorTokenSourcesPool"));
        }


        protected (List<(Type view, Type presenter)> views, List<(Type view, Type presenter)> popups) AutoViewInstall()
        {
            var bindWrapper = typeof(ProjectInstaller)
                .GetMethod("BindWrapper", BindingFlags.Instance | BindingFlags.NonPublic);

            var viewRegistrationList = new List<(Type, Type)>();
            var popupsRegistrationList = new List<(Type, Type)>();

            var bindings = AutoRegisterViewAttribute.GetViews(new[] {typeof(CanvasContainer).Assembly});
            foreach (var binding in bindings)
            {
                var viewType = binding.view;
                var presenterType = viewType.BaseType.GetGenericArguments()[0];

                var bindingMethod = bindWrapper.MakeGenericMethod(viewType, presenterType);
                bindingMethod.Invoke(this, new object[] {binding.path});

                if (CheckForPopupCoroutine(presenterType))
                    popupsRegistrationList.Add((viewType, presenterType));
                else
                    viewRegistrationList.Add((viewType, presenterType));
            }

            return (viewRegistrationList, popupsRegistrationList);
        }

        private bool CheckForPopupCoroutine(Type presenterType)
        {
            var tType = presenterType;
            
            do
            {
                if (tType.IsGenericType && tType.GetGenericTypeDefinition() == typeof(PopupPresenterCoroutine<,,>))
                    return true;

                tType = tType.BaseType;
            }
            while(tType != null);

            return false;
        }

        private void BindWrapper<TView, TPresenter>(string path)
        {
            Container.BindViewPresenter<TView, TPresenter>(path);
        }
    }
}