using System;
using System.Collections.Generic;
using System.IO;
using Data;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Games
{
    public class CapitalCityGameController : MonoBehaviour
    {
        [FormerlySerializedAs("capitalCity")] [SerializeField] private CapitalCityGame capitalCityGame;
        [SerializeField] private CapitalCityItem countryItem;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button backButton; 
       
        private readonly List<CapitalCityItem> _capitalsIcons = new List<CapitalCityItem>();
        private FlagGameAnsweredData _answeredData;
        
        private  string _answeredDataFilePath;
        private CapitalCitiesData _capitalCityData;

        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();

        private void OnEnable()
        {
            capitalCityGame.OnBackButtonClick().Subscribe(_=>capitalCityGame.gameObject.SetActive(false));
            capitalCityGame.OnNextButtonClick.Subscribe(index => RunFlagGame(index+1));
            capitalCityGame.OnPreviousButtonClickSubject.Subscribe(index => RunFlagGame(index-1));
            capitalCityGame.OnComplete.Subscribe(SaveData);
        }

        private void SaveData(string countryName)
        {
            if (_answeredData.AnsweredData.Contains(countryName))
            {
                return;
            }
            _answeredData.AnsweredData.Add(countryName);
            
            File.WriteAllText(_answeredDataFilePath,JsonUtility.ToJson(_answeredData));
        }

        private void RunFlagGame(int index)
        {
            
            capitalCityGame.Init(_capitalCityData.CapitalCityData[index],index,_capitalCityData.CapitalCityData.Count,_answeredData.AnsweredData.Contains(_capitalCityData.CapitalCityData[index].CountryName));
        }

        public void RunGame(CapitalCitiesData capitalCityData)
        {
            _answeredDataFilePath =  Path.Combine(Application.persistentDataPath,"CountryGameData.json");
            _capitalCityData = capitalCityData;
            _answeredData=GetAnsweredData();
            
            InitIcons(capitalCityData);
        }

        private FlagGameAnsweredData GetAnsweredData()
        {
            var answeredData = new FlagGameAnsweredData();

            if (File.Exists(_answeredDataFilePath))
            {
                string jsonData = File.ReadAllText(_answeredDataFilePath);
                Debug.LogError(jsonData);
                answeredData=JsonUtility.FromJson<FlagGameAnsweredData>(jsonData);
            }
            else
            {
                Debug.LogError(_answeredDataFilePath);
                File.Create(_answeredDataFilePath).Dispose();
                File.WriteAllText(_answeredDataFilePath,JsonUtility.ToJson(answeredData));
            }

            return answeredData;
        }

        private void InitIcons(CapitalCitiesData capitalCities)
        {
            for (int i = 0; i < capitalCities.CapitalCityData.Count; i++)
            {
                var data = capitalCities.CapitalCityData[i];
                var icon = Instantiate(countryItem, container);
                var i1 = i;
                icon.OnButtonClickObserver.Subscribe(_ =>
                {
                    capitalCityGame.Init(data, i1, capitalCities.CapitalCityData.Count,
                        _answeredData.AnsweredData.Contains(data.CountryName));
                });
                icon.Init(data.CountryName);
                _capitalsIcons.Add(icon);
            }
        }
    }
}