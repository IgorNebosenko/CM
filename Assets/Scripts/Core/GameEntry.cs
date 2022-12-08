using System.Collections;
using CM.UI.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CM.Core
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private GameObject[] dontDestroyList;
        [SerializeField] private GameObject[] devElementsList;

        private const int MenuSceneId = 1;
        
        private ViewManager _viewManager;

        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Awake()
        {
            _viewManager.ShowView<LoadingPresenter>();
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
            StartCoroutine(EntryStart());
        }

        private IEnumerator EntryStart()
        {
            yield return SceneManager.LoadSceneAsync(MenuSceneId);
        }
    }
}
