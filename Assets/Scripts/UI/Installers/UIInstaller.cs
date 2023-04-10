using Core.UI;
using Tools.UIContainer;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public class UIInstaller : MonoInstaller<UIInstaller>
    {
        [SerializeField] private ViewContainer viewContainer;
        [SerializeField] private PopupContainer popupContainer;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>().AsSingle().NonLazy();
            
            Container.Bind<IUIContainer<ViewContainer>>().FromInstance(viewContainer).AsSingle().NonLazy();
            Container.Bind<IUIContainer<PopupContainer>>().FromInstance(popupContainer).AsSingle().NonLazy();
            
            Container.Bind<IViewPool>().To<ViewPool>().AsSingle().NonLazy();
            Container.Bind<ViewPaths>().FromScriptableObjectResource("ViewPaths").AsSingle().NonLazy();
        }
    }
}