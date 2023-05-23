using System;
using System.Collections.Generic;
using System.IO;
using Data;
using Tools;
using UI.Components;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace UI.Games
{
    public class FlagGameController : MonoBehaviour
    { 
        [SerializeField] private Button backButton; 
        [SerializeField] private FlagGame flagGame;
        [SerializeField] private FlagIcon flagIcon;
        [SerializeField] private RectTransform container;
       
        private readonly List<FlagIcon> _flagIcons = new List<FlagIcon>();
        private FlagsQuizData _flagData;
        private AnsweredData _answeredData;

        private string _answeredDataFilePath;

        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();

        private void OnEnable()
        {
            flagGame.OnBackButtonClick().Subscribe(_=>flagGame.gameObject.SetActive(false));
            flagGame.OnNextButtonClick.Subscribe(index => RunFlagGame(index+1));
            flagGame.OnPreviousButtonClickSubject.Subscribe(index => RunFlagGame(index-1));
            flagGame.OnComplete.Subscribe(SaveData);
        }

        private void SaveData(string countryName)
        {
            if (_answeredData.Data.Contains(countryName))
            {
                return;
            }
            _answeredData.Data.Add(countryName);
            var countryFlag=_flagIcons.Find(c => c.CountryName.Equals(countryName));
            countryFlag.Complete();
            File.WriteAllText(_answeredDataFilePath,JsonUtility.ToJson(_answeredData));
        }

        private void RunFlagGame(int index)
        {
            flagGame.Init(_flagData.FlagData[index],index,_flagData.FlagData.Count,_answeredData.Data.Contains(_flagData.FlagData[index].CountryName));
        }

        public void RunGame(FlagsQuizData flagsQuizData, IAddressableProvider addressableProvider)
        {
            _answeredDataFilePath = Path.Combine(Application.persistentDataPath, "FlagGameData.json");
            _flagData = flagsQuizData;
            _answeredData = GetAnsweredData();
            flagGame.SetData(addressableProvider);
            InitIcons(flagsQuizData);
        }

        private AnsweredData GetAnsweredData()
        {
            var answeredData = new AnsweredData();

            if (File.Exists(_answeredDataFilePath))
            {
                string jsonData = File.ReadAllText(_answeredDataFilePath);
                Debug.LogError(jsonData);
                answeredData=JsonUtility.FromJson<AnsweredData>(jsonData);
            }
            else
            {
                Debug.LogError(_answeredDataFilePath);
                File.Create(_answeredDataFilePath).Dispose();
                File.WriteAllText(_answeredDataFilePath,JsonUtility.ToJson(answeredData));
            }

            return answeredData;
        }

        private void InitIcons(FlagsQuizData flagsQuizData)
        {
            for (int i = 0; i < flagsQuizData.FlagData.Count; i++)
            {
                var data = flagsQuizData.FlagData[i];
                var icon = Instantiate(flagIcon, container);
                var i1 = i;
                icon.OnButtonClick.Subscribe(_ =>
                {
                    flagGame.Init(data, i1, flagsQuizData.FlagData.Count,
                        _answeredData.Data.Contains(data.CountryName));
                });
                icon.Init(data.CountryFlag, _answeredData.Data.Contains(data.CountryName),data.CountryName);
                _flagIcons.Add(icon);
            }
        }
    }
}