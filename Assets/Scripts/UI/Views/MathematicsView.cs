using System;
using System.IO;
using Data;
using Tools;
using UI.Games;
using UI.Games.Quiz;
using UI.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class MathematicsView : View<MathematicsViewModel>
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button progressionGameButton;
        [SerializeField] private Button expressionGameButton;
        [SerializeField] private Button planimetricsGameButton;
        [SerializeField] private AssetReference progressionGameAssetReference;
        [SerializeField] private AssetReference quizGameAssetReference;

        [Inject] private IAddressableProvider _addressableProvider;
        [Inject] private MathematicsGameData _mathematicsGameData;
        
        private FillInQuizController _progressionGame;
        private QuizController _expressionGame;
        private QuizController _quizController;

        protected override void OnEnabled()
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.LogError(Guid.NewGuid().ToString());
            }
            backButton.OnClickAsObservable().Subscribe(_ => ViewModel.OnBackButtonClick()).AddTo(Disposable);
            progressionGameButton.OnClickAsObservable().Subscribe(_ => ShowProgressionGame()).AddTo(Disposable);
            expressionGameButton.OnClickAsObservable().Subscribe(_ => ShowCountExpressionGame()).AddTo(Disposable);
            planimetricsGameButton.OnClickAsObservable().Subscribe(_ => ShowPlanimetricsGame()).AddTo(Disposable);
        }

        private void ShowProgressionGame()
        {
            _addressableProvider.InstantiateAsset(progressionGameAssetReference, transform,_ =>
            {
                _progressionGame = _.GetComponent<FillInQuizController>();
                if (_progressionGame == null)
                {
                    Debug.LogError("Game can not be found!");
                    return;
                }

                _progressionGame.OnBackButtonClick.Subscribe(_=>
                    _addressableProvider.ReleaseAsset(progressionGameAssetReference,_progressionGame.gameObject)).AddTo(Disposable);
                _progressionGame.RunGame(_mathematicsGameData.ProgressionQuizData);
            });
        }

        private void ShowCountExpressionGame()
        {
            _addressableProvider.InstantiateAsset(quizGameAssetReference, transform, _ =>
            {
                _expressionGame = _.GetComponent<QuizController>();
                if (_expressionGame == null)
                {
                    Debug.LogError("Game can not be found!");
                    return;
                }

                var path = Path.Combine(Application.persistentDataPath, "ExpressionsGameData.json");
                _expressionGame.OnBackButtonClick.Subscribe(_ =>
                        _addressableProvider.ReleaseAsset(quizGameAssetReference, _expressionGame.gameObject))
                    .AddTo(Disposable);
                _expressionGame.RunGame(_mathematicsGameData.ExpressionsQuizData, path);
            });
            Debug.LogError("ShowCountExpressionGame");
        }

        private void ShowPlanimetricsGame()
        {
            Debug.LogError("ShowPlanimetricsGame");
            _addressableProvider.InstantiateAsset(quizGameAssetReference, transform, _ =>
            {
                _quizController = _.GetComponent<QuizController>();
                if (_quizController == null)
                {
                    Debug.LogError("Game can not be found!");
                    return;
                }

                var path = Path.Combine(Application.persistentDataPath, "PlanimetricsGameData.json");
                _quizController.OnBackButtonClick.Subscribe(_ =>
                        _addressableProvider.ReleaseAsset(quizGameAssetReference, _quizController.gameObject))
                    .AddTo(Disposable);
                _quizController.RunGame(_mathematicsGameData.PlanimetricsQuizData, path);
            });
        }
    }
}