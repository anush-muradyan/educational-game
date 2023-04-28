using Data;
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
    public class GeographyGameView : View<GeographyGameViewModel>
    {
        [SerializeField] private Button flagGameButton;
        [SerializeField] private AssetReference flagGameAssetReference;
        [SerializeField] private RectTransform container;
        
        [Inject] private IAddressableProvider _addressableProvider;
        [Inject] private FlagsQuizData _flagsQuizData;
        
        private FlagGameController _flagGameController;

        protected override void OnEnabled()
        {
            flagGameButton.OnClickAsObservable().Subscribe(_ => OnFlagGameButtonClick());
        }

        private void OnFlagGameButtonClick()
        {
            _addressableProvider.InstantiateAsset(flagGameAssetReference, container, flagGameContainer =>
            {
                _flagGameController = flagGameContainer.GetComponent<FlagGameController>();
                _flagGameController.RunGame(_flagsQuizData);
                _flagGameController.OnBackButtonClick.Subscribe(_ => ReleaseFlagGameController());

            });
        }

        private void ReleaseFlagGameController()
        {
            if (_flagGameController != null)
            {
                _addressableProvider.ReleaseAsset(flagGameAssetReference, _flagGameController.gameObject);
            }
            
        }

        protected override void OnDestroy()
        {
            ReleaseFlagGameController();
        }
    }
}