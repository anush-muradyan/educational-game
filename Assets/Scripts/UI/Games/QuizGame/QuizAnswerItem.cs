using System;
using Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games.QuizGame
{
    public class QuizAnswerItem : MonoBehaviour
    {
        public IObservable<int> OnSelected => _onSelected;
        private Subject<int> _onSelected = new();

        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private GameObject checkmark;

        public int Id => _id;
        
        private int _id;

        private void Awake() {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() {
            _onSelected?.OnNext(_id);
        }

        public void Init(QuizAnswer answer) {
            _id = answer.Index;
            answerText.text = answer.Answer;
            gameObject.SetActive(true);
            SetState(false);
        }

        public void SetState(bool state) {
            checkmark.SetActive(state);
        }
    }
}