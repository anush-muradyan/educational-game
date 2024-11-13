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
        public const string VIEW_CONTAINER_KEY = "view_container";
        public const string POPUP_CONTAINER_KEY = "popup_container";

        [SerializeField] private string viewsLocation;
        [SerializeField] private string popupsLocation;
        [SerializeField] private RectTransform viewContainer;
        [SerializeField] private RectTransform popupContainer;

        public override void InstallBindings()
        {
            Container.Bind<ViewFactory>().AsSingle().Lazy();

            Container.BindView<GeographyGameView>(viewsLocation);
            Container.BindView<MathematicsView>(viewsLocation);
            Container.BindView<StartGameView>(viewsLocation);


            Container.Bind<RectTransform>().WithId(VIEW_CONTAINER_KEY).FromInstance(viewContainer).Lazy();
            Container.Bind<RectTransform>().WithId(POPUP_CONTAINER_KEY).FromInstance(popupContainer).Lazy();
            Container.Bind<IUiService>().To<UiService>().AsSingle().NonLazy();
            Container.Bind<IPopupService>().To<PopupService>().AsSingle().NonLazy();
        }
    }
}