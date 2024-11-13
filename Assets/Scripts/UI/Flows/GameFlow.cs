using UI.Components;
using UI.Views;
using UniRx;

namespace UI.Flows
{
    public class GameFlow : AbstractFlow
    {
        private readonly FlowNavigator _flowNavigator;
        private readonly IUiService _uiService;

        public GameFlow(FlowNavigator flowNavigator, IUiService uiService)
        {
            _flowNavigator = flowNavigator;
            _uiService = uiService;
        }

        public override void Run()
        {
            var view = _uiService.ShowView<StartGameView>();
            view.OnGeographyGameButtonObservable.Subscribe(_ => _flowNavigator.RunGeographyFlow());
            view.OnMathematicsGameButtonObservable.Subscribe(_ => _flowNavigator.RunMathematicsFlow());
        }
    }
}