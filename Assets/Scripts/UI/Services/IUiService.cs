using UI.Views;

namespace UI.Services
{
    public interface IUiService
    {
        T ShowView<T>() where T : AbstractView;
        void CloseCurrentView();
        AbstractView CurrentView { get; }
        bool HasView { get; }
    }
}

