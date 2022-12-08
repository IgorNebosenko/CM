using System.Collections;
using CM.UI.Views;
using ElectrumGames.MVP;
using UnityEngine.SceneManagement;

namespace CM.UI.Presenters
{
    public class MainMenuPresenter : Presenter<MainMenuView>
    {
        private const int GameSceneId = 2;
        
        public MainMenuPresenter(MainMenuView view) : base(view)
        {
        }

        public IEnumerator ButtonPlayPressedProcess()
        {
            yield return SceneManager.LoadSceneAsync(GameSceneId);
        }
    }
}