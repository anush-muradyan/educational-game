using DefaultNamespace.Core.Shared;

namespace UI.Views
{
    public interface IViewManager
    {
        public AsyncTask<TView> Open<TView>() where TView : IView;
        public void Close<TView>(TView view) where TView : IView;
    }
}