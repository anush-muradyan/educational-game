using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class FillInQuizItem : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private RectTransform completed;
        [SerializeField] private TextMeshProUGUI index;

        public string Id { get; private set; }
        public IObservable<Unit> OnButtonClick => button.OnClickAsObservable();

        public void Init(int number,bool complete,string id)
        {
            index.text = $"Խնդիր {number + 1}";
            Id = id;
            completed.gameObject.SetActive(complete);
        }

        public void SetComplete()
        {
            completed.gameObject.SetActive(true);
        }
    }
}