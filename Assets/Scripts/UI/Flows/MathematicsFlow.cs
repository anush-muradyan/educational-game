using UI.Components;
using UI.Views;
using UniRx;

namespace UI.Flows
{
    public class MathematicsFlow : AbstractFlow
    {
        private readonly IUiService _uiService;
        private readonly FlowNavigator _flowNavigator;

        public MathematicsFlow(IUiService uiService, FlowNavigator flowNavigator)
        {
            _uiService = uiService;
            _flowNavigator = flowNavigator;
        }

        public override void Run()
        {
            var view = _uiService.ShowView<MathematicsView>();
            view.OnBackButtonObservable.Subscribe(_ => _flowNavigator.Start());
        }
    }
}