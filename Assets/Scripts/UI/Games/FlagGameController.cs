using System;
using System.Collections.Generic;
using System.IO;
using Data;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace UI.Games
{
    public class FlagGameController : MonoBehaviour
    {
        [SerializeField] private FlagGame flagGame;
        [SerializeField] private FlagIcon flagIcon;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button backButton; 
       
        private readonly List<FlagIcon> _flagIcons = new List<FlagIcon>();
        private FlagsQuizData _flagData;
        private FlagGameAnsweredData _answeredData;
        
        private  string _answeredDataFilePath =  Path.Combine(Application.persistentDataPath,"FlagGameData.json");

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
            if (_answeredData.AnsweredData.Contains(countryName))
            {
                return;
            }
            _answeredData.AnsweredData.Add(countryName);
            
            File.WriteAllText(_answeredDataFilePath,JsonUtility.ToJson(_answeredData));
        }

        private void RunFlagGame(int index)
        {
            flagGame.Init(_flagData.FlagData[index],index,_flagData.FlagData.Count,_answeredData.AnsweredData.Contains(_flagData.FlagData[index].CountryName));
        }

        public void RunGame(FlagsQuizData flagsQuizData)
        {
            _flagData = flagsQuizData;

            
            _answeredData=GetAnsweredData();
            
            InitIcons(flagsQuizData);
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
                        _answeredData.AnsweredData.Contains(data.CountryName));
                });
                icon.Init(data.CountryFlag, _answeredData.AnsweredData.Contains(data.CountryName));
                _flagIcons.Add(icon);
            }
        }
    }
}