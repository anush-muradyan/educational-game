using UI.Components;
using UI.Views;
using UniRx;

namespace UI.Flows
{
    public class GeographyFlow : AbstractFlow
    {
        private readonly FlowNavigator _flowNavigator;
        private readonly IUiService _uiService;

        public GeographyFlow(FlowNavigator flowNavigator,IUiService uiService)
        {
            _flowNavigator = flowNavigator;
            _uiService = uiService;
        }

        public override void Run()
        {
            var view=_uiService.ShowView<GeographyGameView>();
            view.OnBackButtonObservable.Subscribe(_ => _flowNavigator.Start());
        }
    }
}