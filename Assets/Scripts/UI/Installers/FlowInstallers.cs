using UI.Flows;
using Zenject;

namespace UI.Installers
{
    public class FlowInstallers : MonoInstaller<FlowInstallers>
    {
        public override void InstallBindings()
        {
            Container.Bind<FlowFactory>().AsSingle().NonLazy();
            Container.Bind<FlowNavigator>().AsSingle().NonLazy();
            Container.BindFactory<GameFlow, PlaceholderFactory<GameFlow>>();
            Container.BindFactory<GeographyFlow, PlaceholderFactory<GeographyFlow>>();
            Container.BindFactory<MathematicsFlow, PlaceholderFactory<MathematicsFlow>>();
        }
    }
}