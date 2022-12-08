using CM.UI.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CM.UI.Views
{
    [AutoRegisterView]
    public class GameView : View<GameViewPresenter>
    {
        [SerializeField] private Image deathEffect;
        [SerializeField] private GameObject hideOnDesktop;
    }
}