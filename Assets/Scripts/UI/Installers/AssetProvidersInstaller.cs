using Tools;
using Zenject;

namespace UI.Installers
{
    public class AssetProvidersInstaller:MonoInstaller<AssetProvidersInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IAddressableProvider>().To<AddressableProvider>().AsSingle().NonLazy();
            Container.Bind<IAssetPathProvider>().To<PathProvider>().AsSingle().NonLazy();
        }
    }
}