using System;
using UI.Popups;

namespace UI.Views
{
    public interface IViewPool
    {
        void SpawnView<T>(Action<T> callback) where T : IView;
        void SpawnPopup<T>(Action<T> callback) where T : IPopup;
        void DeSpawn(IView view);
    }
}