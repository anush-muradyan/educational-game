using System;
using System.Collections.Generic;
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

        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();

        private void OnEnable()
        {
            flagGame.OnBackButtonClick().Subscribe(_=>flagGame.gameObject.SetActive(false));
            flagGame.OnNextButtonClick.Subscribe(index => RunFlagGame(index+1));
            flagGame.OnPreviousButtonClickSubject.Subscribe(index => RunFlagGame(index-1));
        }

        private void RunFlagGame(int index)
        {
            flagGame.Init(_flagData.FlagData[index],index,_flagData.FlagData.Count);
        }

        public void RunGame(FlagsQuizData flagsQuizData)
        {
            _flagData = flagsQuizData;
            InitIcons(flagsQuizData);
        }

        private void InitIcons(FlagsQuizData flagsQuizData)
        {
            for (int i = 0; i < flagsQuizData.FlagData.Count; i++)
            {
                var data = flagsQuizData.FlagData[i];
                var icon = Instantiate(flagIcon, container);
                var i1 = i;
                icon.OnButtonClick.Subscribe(_ => { flagGame.Init(data, i1,flagsQuizData.FlagData.Count); });
                icon.Init(data.CountryFlag);
                _flagIcons.Add(icon);
            }
        }
    }
}