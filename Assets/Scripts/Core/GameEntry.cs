using System;
using CM.UI.UI.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private GameObject[] dontDestroyList;
        [SerializeField] private GameObject[] devElementsList;

        private ViewManager _viewManager;
        private LoadingPresenter _loadingPresenter;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Awake()
        {
            _loadingPresenter = _viewManager.ShowView<LoadingPresenter>();
        }

        private void Start()
        {
            for (var i = 0; i < dontDestroyList.Length; i++)
                DontDestroyOnLoad(dontDestroyList[i]);

            foreach (var devElement in devElementsList)
            {
                if (devElement)
                    Destroy(devElement);
            }
            
            EntryStart();
        }

        private void EntryStart()
        {
        }
    }
}
