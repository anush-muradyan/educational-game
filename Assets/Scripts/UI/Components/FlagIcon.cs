using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class FlagIcon : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image flagImage;
        [SerializeField] private Image completeImage;

        public IObservable<Unit> OnButtonClick => button.OnClickAsObservable();

        public void Init(Sprite sprite, bool complete)
        {
            flagImage.sprite = sprite;
            completeImage.gameObject.SetActive(complete);
        }
    }
}