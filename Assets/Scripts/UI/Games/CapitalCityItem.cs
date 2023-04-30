using System;
using Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games
{
    public class CapitalCityItem: MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI countryNameText;

        public IObservable<Unit> OnButtonClickObserver => button.OnClickAsObservable();
       
        private CountryData _countryData;

        public void Init(string countryName)
        {
            countryNameText.text = countryName;
            gameObject.SetActive(true);
        }

    }
}
