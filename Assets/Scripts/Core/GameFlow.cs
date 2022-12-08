using CM.UI.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CM.Core
{
    public class GameFlow : MonoBehaviour
    {
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Awake()
        {
            _viewManager.ShowView<GameViewPresenter>();
        }
    }
}