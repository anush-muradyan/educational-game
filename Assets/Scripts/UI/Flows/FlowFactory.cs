using Zenject;

namespace UI.Flows
{
    public class FlowFactory
    {
        private readonly DiContainer container;

        public FlowFactory(DiContainer container) {
            this.container = container;
        }

        public T CreateFlow<T>() {
            var factory = container.Resolve<PlaceholderFactory<T>>();
            return factory.Create();
        }
    }
}