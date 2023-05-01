using System;
using Data;
using UI.Components;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games
{
    public class CountryInformationHolder : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private CountryItem countryItem;
        [SerializeField] private CountryDataContainer countryDataContainer;
        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();

        public void RunGame(CountriesData countriesData)
        {
            foreach (var data in countriesData.CountryData)
            {
                CreateCountryItem(data);
            }
        }

        private void CreateCountryItem(CountryData data)
        {
            var item = Instantiate(countryItem,container);
            item.Init(data);
            item.OnShowInformationObserver.Subscribe(ShowCountryInformation);
        }

        private void ShowCountryInformation(CountryData countryData)
        {
            countryDataContainer.ShowData(countryData);
            countryDataContainer.gameObject.SetActive(true);
        }
    }
}