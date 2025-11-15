using UI.Flows;
using Zenject;

namespace DI
{
    public class FlowInstallers : MonoInstaller<FlowInstallers>
    {
        public override void InstallBindings()
        {
            BindFlowInfrastructure();
            BindFlowFactories();
        }

        private void BindFlowInfrastructure()
        {
            Container.Bind<FlowFactory>().AsSingle().NonLazy();
            Container.Bind<FlowNavigator>().AsSingle().NonLazy();
        }

        private void BindFlowFactories()
        {
            Container.BindFactory<GameFlow, PlaceholderFactory<GameFlow>>();
            Container.BindFactory<GeographyFlow, PlaceholderFactory<GeographyFlow>>();
            Container.BindFactory<MathematicsFlow, PlaceholderFactory<MathematicsFlow>>();
        }
    }
}