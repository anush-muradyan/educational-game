using System.Collections.Generic;
using DefaultNamespace.Core.Shared;
using UI.Popups;
using UnityEngine;

namespace UI.Views
{
    public class UIManager : IViewManager, IViewRegistry
    {
        private Stack<IView> views = new Stack<IView>();
        private List<IPopup> popups = new List<IPopup>();

        private readonly Transform _viewContainer;
        private readonly IViewPool _viewPool;

        private IView _cubeView;

        public UIManager(IViewPool viewPool)
        {
            _viewPool = viewPool;
        }
        
        public AsyncTask<TView> Open<TView>() where TView : IView
        {
            AsyncTask<TView> task = new AsyncTask<TView>();
            _viewPool.SpawnView<TView>(view => OnViewSpawned(view, in task));
            return task;
        }

        public AsyncTask<TPopup> OpenPopup<TPopup>()
            where TPopup : IPopup
        {
            AsyncTask<TPopup> task = new AsyncTask<TPopup>();
            _viewPool.SpawnPopup<TPopup>(popup =>
            {
                popup.OnHidden.AddListener(OnPopupClosed);
                popup.Show();
                AddPopup(popup);
                task.Success(popup);
            });
            return task;
        }

        public void ClosePopup<TPopup>() where TPopup : IPopup
        {
            var popup = popups.Find(p => p.GetType() == typeof(TPopup));
            if (popup == null)
            {
                return;
            }

            popup.Hide();
            RemovePopup(popup);
        }

        public void ClosePopup(IPopup popup)
        {
            popup.Hide();
        }

        private void OnPopupClosed(IView popup)
        {
            popup.OnHidden.RemoveListener(OnPopupClosed);
            RemovePopup(popup as IPopup);
            _viewPool.DeSpawn(popup);
        }

        private void OnViewSpawned<TView>(TView view, in AsyncTask<TView> task) where TView : IView
        {
            HideLastView();
            view.OnShown.AddListener(openedView => AddView(view));
            view.Show();
            task.Success(view);
        }

        private void HideLastView()
        {
            if (views.Count <= 0)
            {
                return;
            }

            var view = views.Peek();
            view.OnHidden.AddListener(OnViewClosed);
            view.Hide();
            Close(view);
        }

        public void Close<TView>(TView view) where TView : IView
        {
            RemoveView(view);
        }

        public void CloseView(IView view)
        {
            view.OnHidden.AddListener(OnViewClosed);
            view.Hide();
        }

        private void OnViewClosed<TView>(TView view) where TView : IView
        {
            view.OnHidden.RemoveListener(OnViewClosed);
            view.OnShown.RemoveAllListeners();
            _viewPool.DeSpawn(view);
        }

        public void RemoveView<TView>(TView view) where TView : IView
        {
            views.Pop();
        }

        public void AddView<TView>(TView view) where TView : IView
        {
            views.Push(view);
        }

        public void RemovePopup<TPopup>(TPopup popup) where TPopup : IPopup
        {
            popups.Remove(popup);
        }

        public void AddPopup<TPopup>(TPopup popup) where TPopup : IPopup
        {
            popups.Add(popup);
        }

        public void CloseAll()
        {
            ClosePopups();
            CloseViews();
        }

        private void ClosePopups()
        {
            foreach (var popup in popups)
            {
                ClosePopup(popup);
            }

            popups.Clear();
        }

        private void CloseViews()
        {
            foreach (var view in views)
            {
                CloseView(view);
            }

            views.Clear();
        }
    }
}