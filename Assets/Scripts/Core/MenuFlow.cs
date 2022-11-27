using CM.UI.UI.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class MenuFlow : MonoBehaviour
    {
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Awake()
        {
            _viewManager.CloseRootView();
            _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}
