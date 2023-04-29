using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Games
{
    public class FlagIcon : MonoBehaviour
    {
        [SerializeField] private Image flagIcon;
        [SerializeField] private Button button;

        public IObservable<Unit> OnButtonClick => button.OnClickAsObservable();

        public void Init(Sprite sprite, bool contains)
        {
            flagIcon.sprite = sprite;
        }
    }
}