using System;
using Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games
{
    public class CountryItem : MonoBehaviour
    {
        [SerializeField] private Button showInformation;
        [SerializeField] private TextMeshProUGUI countryNameText;

        public IObservable<CountryData> OnShowInformationObserver => _showInformation;
       
        private Subject<CountryData> _showInformation = new Subject<CountryData>();
        private CountryData _countryData;

        private void Start()
        {
            showInformation.OnClickAsObservable().Subscribe(_=>OnShowInformationButtonClick());
        }

        public void Init(CountryData countryData)
        {
            _countryData = countryData;
            countryNameText.text = countryData.CountryName;
            gameObject.SetActive(true);
        }

        private void OnShowInformationButtonClick()
        {
            _showInformation?.OnNext(_countryData);
        }
    }
}