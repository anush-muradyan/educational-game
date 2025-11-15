using DI;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class ViewFactory
    {
        private readonly DiContainer _container;
        private readonly RectTransform _viewContainer;

        public ViewFactory(DiContainer container,
            [Inject(Id = UiInstaller.ViewContainerKey)]
            RectTransform viewContainer)
        {
            _container = container;
            _viewContainer = viewContainer;
        }

        private T Create<T>() where T : AbstractView
        {
            return _container.Resolve<T>();
        }

        public T CreateView<T>() where T : AbstractView
        {
            var view = Create<T>();
            view.transform.SetParent(_viewContainer, false);
            return view;
        }
    }
}