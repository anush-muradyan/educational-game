using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Data;
using TMPro;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UI.Games
{
    public class FillInQuizController : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private FillInQuizItem item;
        [SerializeField] private RectTransform container;
        [SerializeField] private RectTransform completePanel;
        [SerializeField] private FillInQuiz quiz;
        [SerializeField] private TextMeshProUGUI correctAnswerText;

        public IObservable<Unit> OnBackButtonClick => backButton.OnClickAsObservable();

        private List<FillInQuizItem> _icons = new List<FillInQuizItem>();
        private string _answeredDataFilePath;
        private AnsweredData _answeredData;
        private int _currentQuizIndex;
        private List<FillInAnswerData> _data;

        public void RunGame(List<FillInAnswerData> data)
        {
            _answeredDataFilePath = Path.Combine(Application.persistentDataPath, "ProgressionGameData.json");
            _answeredData = GetAnsweredData();
            _data = data;
            for (int i = 0; i < data.Count; i++)
            {
                var icon = Instantiate(item, container);
                _icons.Add(icon);
                icon.Init(i, _answeredData.Data.Contains(data[i].ID),data[i].ID);
                var index = i;
                icon.OnButtonClick.Subscribe(_ => ShowQuestion(index));
            }

            nextButton.OnClickAsObservable().Subscribe(_ => ShowQuestion(_currentQuizIndex + 1));
            previousButton.OnClickAsObservable().Subscribe(_ => ShowQuestion(_currentQuizIndex - 1));
            quiz.Complete.Subscribe(SaveQuizData);
        }

        private void SaveQuizData(string id)
        {
            completePanel.gameObject.SetActive(true);
            correctAnswerText.text = _data[_currentQuizIndex].CorrectAnswer.ToString();
            if (_answeredData.Data.Contains(id))
            {
                return;
            }

            _answeredData.Data.Add(id);

            File.WriteAllText(_answeredDataFilePath, JsonUtility.ToJson(_answeredData));
        }

        private void ShowQuestion(int index)
        {
            var fillInAnswerData = _data[index];
            _currentQuizIndex = index;
            previousButton.gameObject.SetActive(_currentQuizIndex > 0);
            nextButton.gameObject.SetActive(_currentQuizIndex < _data.Count - 1);
            quiz.gameObject.SetActive(true);
            quiz.Init(fillInAnswerData);
            
            var complete = GetAnsweredData().Data.Contains(fillInAnswerData.ID);
            completePanel.gameObject.SetActive(complete);
            if (complete)
            {
                correctAnswerText.text = fillInAnswerData.CorrectAnswer.ToString();
            }
        }

        private AnsweredData GetAnsweredData()
        {
            var answeredData = new AnsweredData();

            if (File.Exists(_answeredDataFilePath))
            {
                string jsonData = File.ReadAllText(_answeredDataFilePath);
                Debug.LogError(jsonData);
                answeredData = JsonUtility.FromJson<AnsweredData>(jsonData);
            }
            else
            {
                Debug.LogError(_answeredDataFilePath);
                File.Create(_answeredDataFilePath).Dispose();
                File.WriteAllText(_answeredDataFilePath, JsonUtility.ToJson(answeredData));
            }

            return answeredData;
        }
    }
}