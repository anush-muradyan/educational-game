using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace UI.Views
{
    public interface IViewModel : IDisposable
    {
        void Reset();
    }

    public abstract class View : MonoBehaviour, IView
    {
        public UnityEvent<IView> OnHidden { get; } = new UnityEvent<IView>();
        public UnityEvent<IView> OnWillHide { get; } = new UnityEvent<IView>();
        public UnityEvent<IView> OnWillShow { get; } = new UnityEvent<IView>();
        public UnityEvent<IView> OnShown { get; } = new UnityEvent<IView>();

        [SerializeField] private bool isCacheable;

        public bool IsCacheable => isCacheable;
        public GameObject ObjectReference => gameObject;

        protected CompositeDisposable Disposable { get; } = new CompositeDisposable();

        #region Unity Methods

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnDestroy()
        {
            Disposable?.Dispose();
        }

        #endregion

        public abstract void ResetView();

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public virtual void Show()
        {
            //OnWillShow?.Invoke(this);
            AnimateShow();
        }

        public virtual void Hide()
        {
            AnimateHide();
        }

        public virtual void AnimateShow()
        {
            //OnShown?.Invoke(this);
        }

        public virtual void AnimateHide()
        {
            //OnHidden?.Invoke(this);
        }
    }

    public abstract class View<TViewModel> : View where TViewModel : IViewModel
    {
        public TViewModel ViewModel { get; private set; }


        [Inject]
        private void Construct(TViewModel model)
        {
            ViewModel = model;
            OnEnabled();
        }

        protected sealed override void OnEnable()
        {
            base.OnEnable();
        }

        protected virtual void OnEnabled()
        {
            ResetViewComponents();
        }

        protected virtual void ResetViewComponents()
        {
        }


        public override void ResetView()
        {
            ViewModel?.Reset();
        }
    }
}