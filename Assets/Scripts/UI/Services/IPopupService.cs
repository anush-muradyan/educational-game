using UI.Popups;

namespace UI.Services
{
    public interface IPopupService
    {
        T ShowPopup<T>() where T : AbstractPopup;
        bool ClosePopup<T>() where T : AbstractPopup;
        void CloseAllPopups<T>() where T : AbstractPopup;
        void CloseAllPopups();
        int ActivePopupCount { get; }
        bool HasPopup<T>() where T : AbstractPopup;
    }
}

