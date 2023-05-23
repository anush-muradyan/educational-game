using System;
using Data;
using DG.Tweening;
using Managers;
using ModestTree;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games
{
    public class FillInQuiz : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button submitButton;
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private RectTransform wrongAnswer;
        [SerializeField] private TMP_InputField answerInputFiled;
       
        private FillInAnswerData _fillInAnswerData;
        public IObservable<string> Complete => _completeSubject;
        private Subject<string> _completeSubject = new Subject<string>();
        private string _id;
        private void Start()
        {
            backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
            submitButton.OnClickAsObservable().Subscribe(_ => OnSubmitButtonClick());
            answerInputFiled.onValueChanged.AddListener(OnInputFieldValueChange);
        }

        private void OnInputFieldValueChange(string arg)
        {
            submitButton.interactable = !answerInputFiled.text.IsEmpty();
        }

        private void OnSubmitButtonClick()
        {
            if (_fillInAnswerData == null )
            {
                Debug.LogError("Data is empty");
                return;
            }
            var answer=float.Parse(answerInputFiled.text);
            Debug.LogError(answer);
            if (answer.Equals(_fillInAnswerData.CorrectAnswer))
            {
                _completeSubject?.OnNext(_id);
                SoundManager.Instance.PlaySFX(SoundName.CorrectAnswer);
                Debug.LogError("Ճիշտ է");
            }
            else
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(wrongAnswer.DOScale(Vector3.one, 0.5f));
                sequence.AppendInterval(0.3f);
                sequence.Append(wrongAnswer.DOScale(Vector3.zero, 0.5f));
                SoundManager.Instance.PlaySFX(SoundName.WrongAnswer);
                Debug.LogError("Սխալ է");
            }
        }

        public void Init(FillInAnswerData fillInAnswerData)
        {
            _id = fillInAnswerData.ID;
            answerInputFiled.text = string.Empty;
            _fillInAnswerData = fillInAnswerData;
            question.text = fillInAnswerData.Question;
        }
    }
}