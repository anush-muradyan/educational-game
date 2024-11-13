using DI;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Components
{
    public class ViewFactory
    {
        private readonly DiContainer _container;
        private readonly RectTransform _viewContainer;
        private readonly RectTransform _popupContainer;

        public ViewFactory(DiContainer container,
            [Inject(Id = UiInstaller.VIEW_CONTAINER_KEY)]
            RectTransform viewContainer,
            [Inject(Id = UiInstaller.POPUP_CONTAINER_KEY)]
            RectTransform popupContainer)
        {
            _container = container;
            _viewContainer = viewContainer;
            _popupContainer = popupContainer;
        }

        private T Create<T>() where T : AbstractView
        {
            return _container.Resolve<T>();
        }

        public T CreateView<T>() where T : AbstractView
        {
            T view = Create<T>();
            view.transform.SetParent(_viewContainer, false);
            return view;
        }

        public T CreatePopup<T>() where T : AbstractView
        {
            T view = Create<T>();
            view.transform.SetParent(_popupContainer, false);
            return view;
        }
    }
}