using System;
using UI.Views;
using UniRx;
using UnityEngine;

namespace UI.ViewModels
{
    public class ViewModel:IViewModel
    {
        private readonly CompositeDisposable compositeDisposable;
        protected BoolReactiveProperty IsBusy { get; private set; }


        protected ViewModel() {
            compositeDisposable = new CompositeDisposable();
            IsBusy = new BoolReactiveProperty();
            AddDisposable(IsBusy);
        }

        protected void AddDisposable(IDisposable disposable) {
            compositeDisposable.Add(disposable);
        }

        public void SetBusy(bool state) {
            IsBusy.Value = state;
        }

        public virtual void Reset() {
            Dispose();
        }

        public virtual void Dispose() {
            Debug.Log($"VIEW {typeof(ViewModel)} Destroyed");
            compositeDisposable?.Dispose();
        }
    }
}