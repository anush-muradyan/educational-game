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
        public int PosIndex;
        private RectTransform _parent;

        private bool _isInitialPos = true;

        private CompositeDisposable _disposable=new CompositeDisposable();
        public IObservable<bool> ButtonClickObservable => buttonClickSubject;
        public RectTransform Container => container;

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
            _parent = transform as RectTransform;
            _isInitialPos = true;
            Letter = letter;
            letterText.text = letter.ToString();
        }

        public Tween SetState(Vector3 newPos,int posIndex)
        {
            PosIndex = posIndex;
            _tween?.Kill();
           _tween = container.transform.DOMove(newPos, 0.3f);
           return _tween;
        }

        private void SetInitialPos()
        {
            _tween?.Kill();
            _tween = container.transform.DOLocalMove(Vector3.zero, 0.3f);
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