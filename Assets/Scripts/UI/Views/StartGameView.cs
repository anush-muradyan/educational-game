using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class StartGameView : AbstractView//<StartGameViewModel>
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private RectTransform startGameContainer;
        [SerializeField] private RectTransform gamesContainer;
        [SerializeField] private Button geographyGame;
        [SerializeField] private Button mathematicsGame;

        public IObservable<Unit> OnGeographyGameButtonObservable => geographyGame.OnClickAsObservable();
        public IObservable<Unit> OnMathematicsGameButtonObservable => mathematicsGame.OnClickAsObservable();
        
        protected void Start()
        {
            startGameButton.OnClickAsObservable().Subscribe(_ => OnStartGameButtonClick()).AddTo(CompositeDisposable);
        }
        
        private void OnStartGameButtonClick()
        {
            startGameContainer.gameObject.SetActive(false);
            gamesContainer.gameObject.SetActive(true);
        }
    }
}