using UI.Components;
using UI.Views;
using UnityEngine;
using Zenject;

namespace DI
{
    public static class DiExtension
    {
        public static void BindView<TView>(this DiContainer container, string location) where TView : AbstractView
        {
            container
                .Bind<TView>()
                .FromComponentInNewPrefabResource($"{location}/{typeof(TView).Name}")
                .AsTransient()
                .Lazy();
        }
    }

    public class UiInstaller : MonoInstaller<UiInstaller>
    {
        public const string ViewContainerKey = "view_container";
        public const string PopupContainerKey = "popup_container";

        [Header("Prefab Locations")]
        [SerializeField] private string viewsLocation;
        [SerializeField] private string popupsLocation;
        
        [Header("UI Containers")]
        [SerializeField] private RectTransform viewContainer;
        [SerializeField] private RectTransform popupContainer;

        public override void InstallBindings()
        {
            BindFactories();
            BindViews();
            BindContainers();
            BindServices();
        }

        private void BindFactories()
        {
            Container.Bind<ViewFactory>().AsSingle().Lazy();
        }

        private void BindViews()
        {
            Container.BindView<GeographyGameView>(viewsLocation);
            Container.BindView<MathematicsView>(viewsLocation);
            Container.BindView<StartGameView>(viewsLocation);
        }

        private void BindContainers()
        {
            Container.Bind<RectTransform>()
                .WithId(ViewContainerKey)
                .FromInstance(viewContainer)
                .Lazy();
            
            Container.Bind<RectTransform>()
                .WithId(PopupContainerKey)
                .FromInstance(popupContainer)
                .Lazy();
        }

        private void BindServices()
        {
            Container.Bind<IUiService>().To<UiService>().AsSingle().NonLazy();
            Container.Bind<IPopupService>().To<PopupService>().AsSingle().NonLazy();
        }
    }
}

