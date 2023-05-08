using UI.ViewModels;
using Zenject;

namespace UI.Installers
{
    public class GeographyGameInstaller : MonoInstaller<GeographyGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GeographyGameViewModel>().AsSingle().NonLazy();
        }
    }
}