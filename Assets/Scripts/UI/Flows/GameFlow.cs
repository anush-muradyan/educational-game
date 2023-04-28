using UI.ViewModels;
using UI.Views;
using UniRx;

namespace UI.Flows
{
    public class GameFlow : AbstractFlow
    {
        private readonly UIManager _uiManager;
        private readonly FlowNavigator _flowNavigator;

        public GameFlow(UIManager uiManager, FlowNavigator flowNavigator)
        {
            _uiManager = uiManager;
            _flowNavigator = flowNavigator;
        }

        public override void Run()
        {
            _uiManager.Open<StartGameView>().OnSuccess(view =>
            {
                view.ViewModel.Result.Subscribe(result =>
                {
                    switch (result)
                    {
                        case StartGameViewModel.ViewResult.Geography:
                            _flowNavigator.RunGeographyFlow();
                            break;
                    }
                });
            });
        }
    }
}