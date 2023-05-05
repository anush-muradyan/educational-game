using Tools;
using UI.Games;
using UI.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class MathematicsView:View<MathematicsViewModel>
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button geometryGameButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private AssetReference geometryGameControllerAssetReference;

        [Inject] private IAddressableProvider _addressableProvider;

        protected override void OnEnabled()
        {
            backButton.OnClickAsObservable().Subscribe(_ => ViewModel.OnBackButtonClick()).AddTo(Disposable);
            geometryGameButton.OnClickAsObservable().Subscribe(_ => ShowGeometryTasks());
        }

        private void ShowGeometryTasks()
        {
            _addressableProvider.InstantiateAsset(geometryGameControllerAssetReference,container,geometryGame=>
            {
                var game = geometryGame.GetComponent<GeometryGameController>();
                game.RunGame();
            });
        }
    }
}