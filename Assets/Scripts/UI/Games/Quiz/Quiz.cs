using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using Managers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games.Quiz
{
    public class Quiz : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button submitButton;
        [SerializeField] private RectTransform complete;
        [SerializeField] private RectTransform wrongAnswer;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TextMeshProUGUI correctAnswerText;
        [SerializeField] private List<QuizAnswerItem> answers;

        public IObservable<string> OnComplete => _onComplete;
        private Subject<string> _onComplete = new Subject<string>();
        
        private Coroutine _showGameToast;
        private int _selectedId;
        private QuizData _quizData;

        private void Start()
        {
            submitButton.OnClickAsObservable().Subscribe(_ => OnSubmitButtonClick());
            backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
        }

        private void OnSubmitButtonClick()
        {
            if (_quizData.CorrectAnswerId == _selectedId)
            {
                Debug.LogError("correct");
                submitButton.interactable = false;
                complete.gameObject.SetActive(true);
                correctAnswerText.text = _quizData.GetCorrectAnswer().Answer;
                _onComplete?.OnNext(_quizData.ID);
                SoundManager.Instance.PlaySFX(SoundName.CorrectAnswer);
                return;
            }
            Sequence sequence = DOTween.Sequence();
            sequence.Append(wrongAnswer.DOScale(Vector3.one, 0.5f));
            sequence.AppendInterval(0.2f);
            sequence.Append(wrongAnswer.DOScale(Vector3.zero, 0.5f));
            
            SoundManager.Instance.PlaySFX(SoundName.WrongAnswer);
        }

        public void InitQuiz(QuizData quiz,bool alreadyComplete)
        {
            _quizData = quiz;
            questionText.text = quiz.Question;
            complete.gameObject.SetActive(alreadyComplete);

            if (alreadyComplete)
            {
                correctAnswerText.text = quiz.GetCorrectAnswer().Answer;
                return;
            }
            for (var i = 0; i < answers.Count; i++)
            {
                var answer = answers[i];
                answer.Init(quiz.Answers[i]);
                answer.OnSelected.AddListener(OnAnswerSelected);
            }
            submitButton.interactable = false;
        }

        private void OnAnswerSelected(int id)
        {
            Debug.LogError(_quizData.CorrectAnswerId == id ? "Correct" : "Wrong");
            submitButton.interactable = true;
            
            foreach (var answer in answers)
            {
                answer.SetState(answer.Id == id);
            }

            _selectedId = id;
        }
    }
}