using System;
using UniRx;
using UnityEngine;

namespace UI.Views
{
    public abstract class AbstractView: MonoBehaviour, IDisposable
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        public virtual void Dispose()
        {
            CompositeDisposable?.Dispose();   
        }
    }
}