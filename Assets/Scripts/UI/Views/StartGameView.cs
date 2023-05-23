using UI.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class StartGameView : View<StartGameViewModel>
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private RectTransform startGameContainer;
        [SerializeField] private RectTransform gamesContainer;
        [SerializeField] private Button geographyGame;
        [SerializeField] private Button mathematicsGame;

        protected override void OnEnabled()
        {
            base.OnEnabled();
            startGameButton.OnClickAsObservable().Subscribe(_ => OnStartGameButtonClick()).AddTo(Disposable);
            geographyGame.OnClickAsObservable().Subscribe(_ => ViewModel.OnGeographyGameButtonClick())
                .AddTo(Disposable);
            mathematicsGame.OnClickAsObservable().Subscribe(_ => ViewModel.OnMathematicsGameButtonClick())
                .AddTo(Disposable);
        }

        private void OnStartGameButtonClick()
        {
            startGameContainer.gameObject.SetActive(false);
            gamesContainer.gameObject.SetActive(true);
        }
    }
}