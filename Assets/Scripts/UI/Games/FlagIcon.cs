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
        public Subject<Unit> onButtonClick = new Subject<Unit>();

        private void OnEnable()
        {
            // button.OnClickAsObservable().Subscribe(_=>
            // {
            //     Debug.LogError("sd");
            //     onButtonClick.OnNext(Unit.Default);
            //     
            // });
        }

        public void Init(Sprite sprite)
        {
            flagIcon.sprite = sprite;
        }
    }
}