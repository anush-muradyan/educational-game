using System;
using Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games
{
    public class CountryDataContainer : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Image flag;
        [SerializeField] private Image coatOfArms;
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private TextMeshProUGUI capitalCity;
        [SerializeField] private TextMeshProUGUI nationalLanguage;
        [SerializeField] private TextMeshProUGUI areaCount;
        [SerializeField] private TextMeshProUGUI populationCount;
        [SerializeField] private TextMeshProUGUI monetaryUnit;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private RectTransform content;

        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();

        private void Start()
        {
            backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
        }

        public void ShowData(CountryData countryData)
        {
            content.transform.localPosition=Vector3.zero;
            countryName.text = countryData.CountryName;
            capitalCity.text = countryData.CapitalCity;
            areaCount.text = countryData.AreaCount;
            nationalLanguage.text = countryData.NationalLanguage;
            populationCount.text = countryData.PopulationCount;
            monetaryUnit.text = countryData.MonetaryUnit;
            description.text = countryData.Description;
            flag.sprite = countryData.Flag;
            coatOfArms.sprite = countryData.CoatOfArms;
        }
    }
}