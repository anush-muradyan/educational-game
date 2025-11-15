using DI;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    public class PopupFactory
    {
        private readonly DiContainer _container;
        private readonly RectTransform _popupContainer;

        public PopupFactory(DiContainer container,
            [Inject(Id = UiInstaller.PopupContainerKey)]
            RectTransform popupContainer)
        {
            _container = container;
            _popupContainer = popupContainer;
        }

        private T Create<T>() where T : AbstractPopup
        {
            return _container.Resolve<T>();
        }

        public T CreatePopup<T>() where T : AbstractPopup
        {
            var view = Create<T>();
            view.transform.SetParent(_popupContainer, false);
            return view;
        }
    }
}