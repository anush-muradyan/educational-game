using System;
using System.Collections.Generic;
using System.IO;
using Data;
using UI.Components;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games.Quiz
{
    public class QuizController : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private FillInQuizItem quizItem;
        [SerializeField] private RectTransform container;
        [SerializeField] private Quiz quiz;
        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();
        private AnsweredData _answeredData;
        private List<QuizData> _data;
        private List<FillInQuizItem> _items = new List<FillInQuizItem>();
        private int _currentDataIndex;

        public void RunGame(List<QuizData> quizData, string filePath)
        {
            _answeredData = GetAnsweredData(filePath);
            _data = quizData;
            for (int i = 0; i < quizData.Count; i++)
            {
                var item = Instantiate(quizItem, container);
                var i1 = i;
                item.Init(i, _answeredData.Data.Contains(quizData[i].ID),quizData[i].ID);
                item.OnButtonClick.Subscribe(_ => RunQuizGame(i1, filePath));
                _items.Add(item);
            }
            quiz.OnComplete.Subscribe(id => SaveQuizInfo(id, filePath));

            nextButton.OnClickAsObservable().Subscribe(_ => RunQuizGame(_currentDataIndex + 1, filePath));
            previousButton.OnClickAsObservable().Subscribe(_ => RunQuizGame(_currentDataIndex - 1, filePath));
        }

        private void RunQuizGame(int index, string filePath)
        {
            var quizData = _data[index];
            _currentDataIndex = index;
            previousButton.gameObject.SetActive(_currentDataIndex > 0);
            nextButton.gameObject.SetActive(_currentDataIndex < _data.Count - 1);
            quiz.InitQuiz(quizData, _answeredData.Data.Contains(quizData.ID));
            quiz.gameObject.SetActive(true);
        }

        private void SaveQuizInfo(string dataID, string filePath)
        {
            var item = _items.Find(item => item.Id.Equals(dataID));
            Debug.LogError(item.Id);
            item.SetComplete();
            if (_answeredData.Data.Contains(dataID))
            {
                return;
            }
            _answeredData.Data.Add(dataID);
            File.WriteAllText(filePath, JsonUtility.ToJson(_answeredData));
        }
        private AnsweredData GetAnsweredData(string filePath)
        {
            var answeredData = new AnsweredData();

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                Debug.LogError(jsonData);
                answeredData = JsonUtility.FromJson<AnsweredData>(jsonData);
            }
            else
            {
                Debug.LogError(filePath);
                File.Create(filePath).Dispose();
                File.WriteAllText(filePath, JsonUtility.ToJson(answeredData));
            }

            return answeredData;
        }
    }
}