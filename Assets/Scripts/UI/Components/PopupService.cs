using System.Collections.Generic;
using System.Linq;
using UI.Views;
using UnityEngine;

namespace UI.Components
{
    public interface IPopupService
    {
        T ShowPopup<T>() where T : AbstractView;
        bool CloseFirstPopup<T>() where T : AbstractView;
        bool CloseLastPopup<T>() where T : AbstractView;
        // UniTask<AlertPopupResult> ShowAlertPopup(AlertInfo info);
    }

    public class PopupService : IPopupService
    {
        private readonly ViewFactory _viewFactory;
    
        private readonly List<AbstractView> _activeViews;
    
        public PopupService(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            _activeViews = new List<AbstractView>();
        }
    
        public T ShowPopup<T>() where T : AbstractView
        {
            T view = _viewFactory.CreatePopup<T>();
            _activeViews.Add(view);
            return view;
        }
    
        // public async UniTask<AlertPopupResult> ShowAlertPopup(AlertInfo info)
        // {
        //     AlertPopup popup = ShowPopup<AlertPopup>();
        //     popup.SetData(info);
        //     AlertPopupResult result = await popup.Result;
        //     if (result is AlertPopupResult.Ok or AlertPopupResult.Cancel)
        //     {
        //         CloseLastPopup<AlertPopup>();
        //     }
        //
        //     return result;
        // }
    
        public bool CloseFirstPopup<T>() where T : AbstractView
        {
            T popup = _activeViews.OfType<T>().FirstOrDefault();
            if (popup == null)
            {
                return false;
            }
    
            _activeViews.Remove(popup);
            Object.Destroy(popup.gameObject);
            return true;
        }
    
        public bool CloseLastPopup<T>() where T : AbstractView
        {
            T popup = _activeViews.OfType<T>().LastOrDefault();
            if (popup == null)
            {
                return false;
            }
    
            _activeViews.Remove(popup);
            Object.Destroy(popup.gameObject);
            return true;
        }
    }
}