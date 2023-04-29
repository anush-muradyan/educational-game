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
            nationalLanguage.text = countryData.NationalLanguage;
            description.text = countryData.Description;
            flag.sprite = countryData.Flag;
            coatOfArms.sprite = countryData.CoatOfArms;
        }
    }
}