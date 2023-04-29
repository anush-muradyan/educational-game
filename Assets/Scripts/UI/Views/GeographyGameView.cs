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
        [SerializeField] private Button informationButton;
        [SerializeField] private AssetReference flagGameAssetReference;
        [SerializeField] private AssetReference informationAssetReference;
        [SerializeField] private RectTransform container;
        
        [Inject] private IAddressableProvider _addressableProvider;
        [Inject] private FlagsQuizData _flagsQuizData;
        [Inject]private CountriesData _countriesData;
        
        private FlagGameController _flagGameController;
        private CountryInformationHolder _informationContainer;

        protected override void OnEnabled()
        {
            flagGameButton.OnClickAsObservable().Subscribe(_ => OnFlagGameButtonClick()).AddTo(Disposable);
            informationButton.OnClickAsObservable().Subscribe(_ => OnInformationButtonClick()).AddTo(Disposable);
        }

        private void OnInformationButtonClick()
        {
            _addressableProvider.InstantiateAsset(informationAssetReference, container, information =>
            {
                _informationContainer = information.GetComponent<CountryInformationHolder>();
                _informationContainer.RunGame(_countriesData);
                _informationContainer.OnBackButtonClick.Subscribe(_ => ReleaseCountryInformationHolder()).AddTo(Disposable);
            });
        }

        private void OnFlagGameButtonClick()
        {
            _addressableProvider.InstantiateAsset(flagGameAssetReference, container, flagGameContainer =>
            {
                _flagGameController = flagGameContainer.GetComponent<FlagGameController>();
                _flagGameController.RunGame(_flagsQuizData);
                _flagGameController.OnBackButtonClick.Subscribe(_ => ReleaseFlagGameController()).AddTo(Disposable);
            });
        }

        private void ReleaseCountryInformationHolder()
        {
            if (_informationContainer != null)
            {
                _addressableProvider.ReleaseAsset(informationAssetReference, _informationContainer.gameObject);
            }
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