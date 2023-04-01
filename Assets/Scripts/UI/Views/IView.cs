using UnityEngine;
using UnityEngine.Events;

namespace UI.Views
{
    public interface IView
    {
        bool IsCacheable { get; }
        GameObject ObjectReference { get; }
        UnityEvent<IView> OnHidden { get; }
        UnityEvent<IView> OnWillHide { get; }
        UnityEvent<IView> OnWillShow { get; }
        UnityEvent<IView> OnShown { get; }
        
        void Show();
        void Hide();
        void ResetView();
    }
}