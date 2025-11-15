using System;
using UI.Views;
namespace UI.Services
{
    public class UiService : IUiService
    {
        private readonly ViewFactory _viewFactory;
        private AbstractView _currentView;

        public AbstractView CurrentView => _currentView;
        public bool HasView => _currentView != null;

        public UiService(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        public T ShowView<T>() where T : AbstractView
        {
            CloseCurrentView();
            
            var view = _viewFactory.CreateView<T>();
            _currentView = view;
            
            return view;
        }

        public void CloseCurrentView()
        {
            if (_currentView == null)
            {
                return;
            }

            if (_currentView.gameObject != null)
            {
                UnityEngine.Object.Destroy(_currentView.gameObject);
            }

            _currentView = null;
        }
    }
}

