using ProjectStartUp;
using Zenject;

namespace DI
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainSceneStartup>().AsSingle().NonLazy();
        }
    }
}