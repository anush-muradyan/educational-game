using UI.Views;
using UnityEngine;

namespace UI.Components
{
    public interface IUiService
    {
        T ShowView<T>() where T : AbstractView;
    }
    
    public class UiService : IUiService
    {
        private readonly ViewFactory _viewFactory;

        private AbstractView _currentView;

        public UiService(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public T ShowView<T>() where T : AbstractView
        {
            CloseCurrent();
            T view = _viewFactory.CreateView<T>();
            _currentView = view;
            return view;
        }

        private void CloseCurrent()
        {
            if (_currentView == null)
            {
                return;
            }

            Object.Destroy(_currentView.gameObject);
        }
    }
    
}