using System;
using System.Collections.Generic;
using Data;
using Pooling;
using TMPro;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UI.Games
{
    public class FlagGame : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Image flagImage;
        [SerializeField] private LetterIcon letterIcon;
        [SerializeField] private RectTransform container;
        [SerializeField] private RectTransform countryNameContainer;
        [SerializeField] private RectTransform correctPanel;
        [SerializeField] private RectTransform uiBlocker;
        [SerializeField] private TextMeshProUGUI countryNameText;
        [SerializeField] private AnimateVibrate animateVibrate;
        [SerializeField] private AnimateVibrate animateVibrateV2;

        private readonly List<LetterIcon> _clickedAnswers = new List<LetterIcon>();
        private readonly List<LetterIcon> _answerItems = new List<LetterIcon>();
        private readonly List<LetterIcon> _letters = new List<LetterIcon>();

        private Dictionary<int, LetterIcon> _userAnswers = new Dictionary<int, LetterIcon>();
        private FlagData _currentFlagData;
        private int _currentDataIndex;
        private CompositeDisposable _disposable;

        public IObservable<string> OnComplete => onComplete;
        public IObservable<int> OnNextButtonClick => onNextButtonClickSubject;
        public IObservable<int> OnPreviousButtonClickSubject => onPreviousButtonClickSubject;
        private Subject<int> onNextButtonClickSubject = new Subject<int>();
        private Subject<int> onPreviousButtonClickSubject = new Subject<int>();
        private Subject<string> onComplete = new Subject<string>();

        private PoolFactory<LetterIcon> _lettersFactory;
        private PoolFactory<LetterIcon> _answerFactory;
        private List<int> _freePlaces = new List<int>();

        public IObservable<Unit> OnBackButtonClick() => backButton.OnClickAsObservable();

        private void Start()
        {
            nextButton.OnClickAsObservable().Subscribe(_ => onNextButtonClickSubject.OnNext(_currentDataIndex));
            previousButton.OnClickAsObservable().Subscribe(_ => onPreviousButtonClickSubject.OnNext(_currentDataIndex));
        }

        public void Init(FlagData data, int dataIndex, int flagsCount, bool alreadyComplete)
        {
            ResetGame();
            InitButtons(dataIndex, flagsCount);
            _lettersFactory ??= new PoolFactory<LetterIcon>(letterIcon, container, 20);
            _answerFactory ??= new PoolFactory<LetterIcon>(letterIcon, countryNameContainer, 20);
            _disposable = new CompositeDisposable();

            _currentFlagData = data;
            _currentDataIndex = dataIndex;
            flagImage.sprite = _currentFlagData.CountryFlag;
            ShowComplete(alreadyComplete);
            if (alreadyComplete)
            {
                gameObject.SetActive(true);
                return;
            }

            InitLetters(_currentFlagData.Letters);
            CreateAnswerIcons(_currentFlagData.CountryName);
            gameObject.SetActive(true);
        }

        private void ShowComplete(bool complete)
        {
            correctPanel.gameObject.SetActive(complete);
        }

        private void InitButtons(int dataIndex, int flagsCount)
        {
            nextButton.gameObject.SetActive(dataIndex + 1 < flagsCount);
            previousButton.gameObject.SetActive(dataIndex > 0);
        }


        private void CreateAnswerIcons(string letters)
        {
            for (int i = 0; i < letters.Length; i++)
            {
                var l = _answerFactory.Get();
                l.transform.SetSiblingIndex(i);
                _answerItems.Add(l);
                var i1 = i;
                l.ButtonClickObservable.Subscribe(_ => { Debug.LogError(letters[i1]); });
            }
        }

        private void InitLetters(List<char> letters)
        {
            for (int i = 0; i < letters.Count; i++)
            {
                var letter = letters[i];
                var l = _lettersFactory.Get();
                _letters.Add(l);
                l.transform.SetSiblingIndex(i);
                l.Init(letter);
                l.ButtonClickObservable.Subscribe(isFromInitialPos => OnLetterIconClick(isFromInitialPos, l)).AddTo(_disposable);
            }
        }

        private void OnLetterIconClick(bool isFromInitialPos, LetterIcon letterItem)
        {
            if (isFromInitialPos)
            {
                if (_clickedAnswers.Count >= _currentFlagData.CountryName.Length)
                {
                    return;
                }

                _clickedAnswers.Add(letterItem);
                var insertPos = _clickedAnswers.Count - 1;
                if (_freePlaces != null && _freePlaces.Count > 0)
                {
                    _freePlaces.Sort();
                    if (_freePlaces[0] <= insertPos)
                    {
                        insertPos = _freePlaces[0];
                        _freePlaces.RemoveAt(0);
                    }
                }

                _userAnswers.Add(insertPos, letterItem);
                letterItem.SetState(_answerItems[insertPos].transform.position, insertPos);
                InitAnswerLetter();
            }
            else
            {
                _userAnswers.Remove(letterItem.PosIndex);
                _freePlaces.Add(letterItem.PosIndex);
                _clickedAnswers.Remove(letterItem);
            }
        }

        private void InitAnswerLetter()
        {
            if (_clickedAnswers.Count == _currentFlagData.CountryName.Length)
            {
                CheckForRight();
            }
        }

        private void CheckForRight()
        {
            for (int i = 0; i < _currentFlagData.CountryName.Length; i++)
            {
                if (!_userAnswers.ContainsKey(i))
                {
                    ShowWrongAnswer();
                    return;
                }

            }

            uiBlocker.gameObject.SetActive(true);
            countryNameText.text = _currentFlagData.CountryName;
            container.gameObject.SetActive(false);
            countryNameContainer.gameObject.SetActive(false);

            SaveCompleteData();
            ShowComplete(true);
        }

        private void SaveCompleteData()
        {
            onComplete?.OnNext(_currentFlagData.CountryName);
        }

        [ContextMenu("animate")]
        private void ShowWrongAnswer()
        {
            // animateVibrate.Animate();
            // animateVibrateV2.Animate();
        }

        private void ReleaseFactoryItems(List<LetterIcon> poolFactoryItems, PoolFactory<LetterIcon> factory)
        {
            if (poolFactoryItems == null || factory == null)
            {
                return;
            }

            foreach (var item in poolFactoryItems)
            {
                factory.Release(item);
            }

            poolFactoryItems?.Clear();
        }

        private void ResetGame()
        {
            foreach (var answer in _clickedAnswers)
            {
                answer.ResetPosition();
            }

            _freePlaces?.Clear();
            ReleaseFactoryItems(_letters, _lettersFactory);
            ReleaseFactoryItems(_answerItems, _answerFactory);
            _disposable?.Dispose();
            _clickedAnswers?.Clear();
            uiBlocker.gameObject.SetActive(false);
            container.gameObject.SetActive(true);
            countryNameContainer.gameObject.SetActive(true);
        }
    }
}