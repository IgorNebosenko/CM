using System;
using CM.UI.UI.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CM.UI
{
    [AutoRegisterView]
    public class MainMenuView : View<MainMenuPresenter>
    {
        [SerializeField] private Button buttonPlay;

        private void OnEnable()
        {
            buttonPlay.onClick.AddListener(() => StartCoroutine(Presenter.ButtonPlayPressedProcess()));
        }

        private void OnDisable()
        {
            buttonPlay.onClick.RemoveListener(() => StartCoroutine(Presenter.ButtonPlayPressedProcess()));
        }
    }
}
