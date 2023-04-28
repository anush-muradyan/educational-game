using UI.ViewModels;
using Zenject;

namespace UI.Installers
{
    public class StartGameViewInstaller:MonoInstaller<StartGameViewInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StartGameViewModel>().AsSingle().NonLazy();
        }
    }
}