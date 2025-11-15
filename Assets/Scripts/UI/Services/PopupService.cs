using System;
using System.Collections.Generic;
using System.Linq;
using UI.Popups;

namespace UI.Services
{
    public class PopupService : IPopupService
    {
        private readonly PopupFactory _popupFactory;
        private readonly List<AbstractPopup> _activePopups;

        public int ActivePopupCount => _activePopups.Count;

        public PopupService(PopupFactory popupFactory)
        {
            _popupFactory = popupFactory ?? throw new ArgumentNullException(nameof(popupFactory));
            _activePopups = new List<AbstractPopup>();
        }

        public T ShowPopup<T>() where T : AbstractPopup
        {
            var popup = _popupFactory.CreatePopup<T>();
            _activePopups.Add(popup);
            return popup;
        }

        public bool ClosePopup<T>() where T : AbstractPopup
        {
            var popup = _activePopups.OfType<T>().LastOrDefault();
            return DestroyPopup(popup);
        }

        public void CloseAllPopups<T>() where T : AbstractPopup
        {
            var popups = _activePopups.OfType<T>().ToList();
            popups.ForEach(p => DestroyPopup(p));
        }

        public void CloseAllPopups()
        {
            var popups = _activePopups.ToList();
            popups.ForEach(p => DestroyPopup(p));
        }

        public bool HasPopup<T>() where T : AbstractPopup
        {
            return _activePopups.OfType<T>().Any();
        }

        private bool DestroyPopup(AbstractPopup popup)
        {
            if (popup == null) return false;

            _activePopups.Remove(popup);
            
            if (popup.gameObject != null)
                UnityEngine.Object.Destroy(popup.gameObject);
            
            return true;
        }
    }
}

