using Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Games.Quiz
{
    public class QuizAnswerItem : MonoBehaviour
    {
        public UnityEvent<int> OnSelected { get; } = new UnityEvent<int>();
        
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private GameObject checkmark;

        public int Id => _id;
        
        private int _id;

        private void Awake() {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() {
            OnSelected?.Invoke(_id);
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