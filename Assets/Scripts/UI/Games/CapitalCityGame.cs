using System;
using System.Collections.Generic;
using Data;
using Pooling;
using TMPro;
using UI.Components;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using RandomNumberGenerator = UnityEngine.Random;

namespace UI.Games
{
    public class CapitalCityGame : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private LetterIcon letterIcon;
        [SerializeField] private TextMeshProUGUI countryText;
        [SerializeField] private RectTransform container;
        [SerializeField] private RectTransform countryNameContainer;
        [SerializeField] private RectTransform correctPanel;
        [SerializeField] private RectTransform uiBlocker;
        [SerializeField] private TextMeshProUGUI countryNameText;

        private readonly List<LetterIcon> _clickedAnswers = new List<LetterIcon>();
        private readonly List<LetterIcon> _answerItems = new List<LetterIcon>();
        private readonly List<LetterIcon> _letters = new List<LetterIcon>();
        private Dictionary<int, LetterIcon> _userAnswers = new Dictionary<int, LetterIcon>();
        private List<int> _freePlaces = new List<int>();

        private int _currentDataIndex;
        private CompositeDisposable _disposable;
        private readonly List<char> _randomLetters = new List<char>()
        {
            'Ա', 'Բ', 'Գ', 'Դ', 'Ե', 'Զ', 'Է',
            'Ը', 'Թ', 'Ժ', 'Ի', 'Լ', 'Խ', 'Ծ',
            'Կ', 'Հ', 'Ձ', 'Ղ', 'Ճ', 'Մ', 'Յ',
            'Ն', 'Շ', 'Ո', 'Չ', 'Պ', 'Ջ', 'Ռ',
            'Ս', 'Վ', 'Տ', 'Ր', 'Ց', 'Փ', 'Ք',
            'Օ', 'Ֆ'
        };

        public IObservable<string> OnComplete => onComplete;
        public IObservable<int> OnNextButtonClick => onNextButtonClickSubject;
        public IObservable<int> OnPreviousButtonClickSubject => onPreviousButtonClickSubject;
        private Subject<int> onNextButtonClickSubject = new Subject<int>();
        private Subject<int> onPreviousButtonClickSubject = new Subject<int>();
        private Subject<string> onComplete = new Subject<string>();

        private PoolFactory<LetterIcon> _lettersFactory;
        private PoolFactory<LetterIcon> _answerFactory;
        private CapitalCityData _capitalCityData;

        public IObservable<Unit> OnBackButtonClick() => backButton.OnClickAsObservable();

        private void Start()
        {
            nextButton.OnClickAsObservable().Subscribe(_ => onNextButtonClickSubject.OnNext(_currentDataIndex));
            previousButton.OnClickAsObservable().Subscribe(_ => onPreviousButtonClickSubject.OnNext(_currentDataIndex));
        }

        public void Init(CapitalCityData capitalCityData, int index, int count, bool alreadyComplete)
        {
            ResetGame();
            InitButtons(index, count);
            _lettersFactory ??= new PoolFactory<LetterIcon>(letterIcon, container, 20);
            _answerFactory ??= new PoolFactory<LetterIcon>(letterIcon, countryNameContainer, 20);
            _disposable = new CompositeDisposable();
            
            countryText.text = capitalCityData.CountryName;
            _capitalCityData = capitalCityData;
            _currentDataIndex = index;
            
            ShowComplete(alreadyComplete);
            if (alreadyComplete)
            {
                gameObject.SetActive(true);
                return;
            }

            InitLetters(_capitalCityData.CapitalCityName,_capitalCityData.ItemsCount);
            CreateAnswerIcons(_capitalCityData.CapitalCityName);
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

        private void InitLetters(string capitalName,int count)
        {
            var list = RandomizeElements(capitalName, count);
            for (int i = 0; i < list.Count; i++)
            {
                var letter = list[i];
                var l = _lettersFactory.Get();
                _letters.Add(l);
                l.transform.SetSiblingIndex(i);
                l.Init(letter);
                l.ButtonClickObservable.Subscribe(isFromInitialPos =>
                {
                    OnLetterIconClick(isFromInitialPos, l);
                }).AddTo(_disposable);
            }
        }

        private List<char> RandomizeElements(string letters, int count)
        {
            var list = new List<char>(count);
            list.AddRange(letters);

            for (int i = letters.Length; i < count; i++)
            {
                int index = RandomNumberGenerator.Range(0, _randomLetters.Count - 1);

                list.Add(_randomLetters[index]);
            }

            Random random = new Random();

            for (int i = list.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (list[j], list[i]) = (list[i], list[j]);
            }

            return list;
        }
        
        private void OnLetterIconClick(bool isFromInitialPos, LetterIcon letterItem)
        {
            if (isFromInitialPos)
            {
                if (_clickedAnswers.Count >= _capitalCityData.CapitalCityName.Length)
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
            if (_clickedAnswers.Count == _capitalCityData.CapitalCityName.Length)
            {
                CheckForRight();
            }
        }

        private void CheckForRight()
        {
            for (int i = 0; i < _capitalCityData.CapitalCityName.Length; i++)
            {
                if (!_userAnswers.ContainsKey(i))
                {
                    ShowWrongAnswer();
                    return;
                }

            }

            uiBlocker.gameObject.SetActive(true);
            countryNameText.text = _capitalCityData.CountryName;
            container.gameObject.SetActive(false);
            countryNameContainer.gameObject.SetActive(false);

            SaveCompleteData();
            ShowComplete(true);
        }

        private void SaveCompleteData()
        {
            onComplete?.OnNext(_capitalCityData.CountryName);
        }


        private void ShowWrongAnswer()
        {

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