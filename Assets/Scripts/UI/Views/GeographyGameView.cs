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
        [SerializeField] private Button capitalCityGame;
        [SerializeField] private Button informationButton;
        [SerializeField] private AssetReference flagGameAssetReference;
        [SerializeField] private AssetReference capitalCityGameAssetReference;
        [SerializeField] private AssetReference informationAssetReference;
        [SerializeField] private RectTransform container;

        [Inject] private IAddressableProvider _addressableProvider;
        [Inject] private FlagsQuizData _flagsQuizData;
        [Inject] private CapitalCitiesData _capitalCitiesData;
        [Inject] private CountriesData _countriesData;

        private FlagGameController _flagGameController;
        private CountryInformationHolder _informationContainer;
        private CapitalCityGameController _capitalCityGameController;

        protected override void OnEnabled()
        {
            flagGameButton.OnClickAsObservable().Subscribe(_ => OnFlagGameButtonClick()).AddTo(Disposable);
            capitalCityGame.OnClickAsObservable().Subscribe(_ => OnCapitalCityGameButtonClick()).AddTo(Disposable);
            informationButton.OnClickAsObservable().Subscribe(_ => OnInformationButtonClick()).AddTo(Disposable);
        }

        private void OnFlagGameButtonClick()
        {
            _addressableProvider.InstantiateAsset(flagGameAssetReference, container, flagGameContainer =>
            {
                _flagGameController = flagGameContainer.GetComponent<FlagGameController>();
                _flagGameController.RunGame(_flagsQuizData,_addressableProvider);
                _flagGameController.OnBackButtonClick.Subscribe(_ => ReleaseFlagGameController()).AddTo(Disposable);
            });
        }

        private void OnCapitalCityGameButtonClick()
        {
            _addressableProvider.InstantiateAsset(capitalCityGameAssetReference, container, capitalCityGameContainer =>
            {
                _capitalCityGameController = capitalCityGameContainer.GetComponent<CapitalCityGameController>();
                _capitalCityGameController.RunGame(_capitalCitiesData);
                _capitalCityGameController.OnBackButtonClick.Subscribe(_ => ReleaseCapitalCityGameController()).AddTo(Disposable);
            });
        }

        private void OnInformationButtonClick()
        {
            _addressableProvider.InstantiateAsset(informationAssetReference, container, information =>
            {
                _informationContainer = information.GetComponent<CountryInformationHolder>();
                _informationContainer.RunGame(_countriesData);
                _informationContainer.OnBackButtonClick.Subscribe(_ => ReleaseCountryInformationHolder())
                    .AddTo(Disposable);
            });
        }

        private void ReleaseCapitalCityGameController()
        {
            if (_capitalCityGameController != null)
            {
                _addressableProvider.ReleaseAsset(capitalCityGameAssetReference, _capitalCityGameController.gameObject);
            }
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