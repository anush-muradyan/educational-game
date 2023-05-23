using UI.ViewModels;
using Zenject;

namespace UI.Installers
{
    public class MathematicsGameViewInstaller:MonoInstaller<MathematicsGameViewInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MathematicsViewModel>().AsSingle().NonLazy();
        }
    }
}