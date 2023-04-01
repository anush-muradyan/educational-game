namespace UI.Views
{
    public interface IViewRegistry
    {
        public void AddView<TView>(TView view) where TView : IView;
        public void RemoveView<TView>(TView view) where TView : IView;
    }
}