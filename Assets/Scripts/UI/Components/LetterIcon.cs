using System;
using DG.Tweening;
using Pooling;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class LetterIcon : MonoBehaviour, IPoolObject
    {
        [SerializeField] private TextMeshProUGUI letterText;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform container;
        
        public char Letter;

        private bool _isInitialPos = true;

        private CompositeDisposable _disposable=new CompositeDisposable();
        public IObservable<bool> ButtonClickObservable => buttonClickSubject;
        private Subject<bool> buttonClickSubject=new Subject<bool>();
        private Tween _tween;

        private void Awake()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => OnButtonClick()).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        private void OnButtonClick()
        {
            buttonClickSubject.OnNext(_isInitialPos);
            if (!_isInitialPos)
            {
                SetInitialPos();
            }

            _isInitialPos = !_isInitialPos;
        }

        public void Init(char letter)
        {
            _isInitialPos = true;
            Letter = letter;
            letterText.text = letter.ToString();
        }

        public void SetState(Vector3 newPos)
        {
            _tween?.Kill();
           _tween= container.transform.DOMove(newPos, 0.3f);
        }

        private void SetInitialPos()
        {
            _tween?.Kill();
            _tween=container.transform.DOLocalMove(Vector3.zero, 0.3f);
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void ResetPosition()
        {
            container.transform.localPosition = Vector3.zero;
        }
    }
}