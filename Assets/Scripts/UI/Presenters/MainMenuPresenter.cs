using System.Collections;
using ElectrumGames.MVP;
using UnityEngine.SceneManagement;

namespace CM.UI.UI.Presenters
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
            Close();
        }
    }
}