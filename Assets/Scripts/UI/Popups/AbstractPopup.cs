using System;
using UniRx;
using UnityEngine;

namespace UI.Popups
{
    public class AbstractPopup : MonoBehaviour, IDisposable
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        public virtual void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}