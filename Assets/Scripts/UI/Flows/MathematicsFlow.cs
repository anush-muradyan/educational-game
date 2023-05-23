using UI.ViewModels;
using UI.Views;
using UniRx;
namespace UI.Flows
{
    public class MathematicsFlow : AbstractFlow
    {
        private readonly UIManager _uiManager;
        private readonly FlowNavigator _flowNavigator;

        public MathematicsFlow(UIManager uiManager,FlowNavigator flowNavigator)
        {
            _uiManager = uiManager;
            _flowNavigator = flowNavigator;
        }

        public override void Run()
        {
            _uiManager.Open<MathematicsView>().OnSuccess(view =>
            {
                view.ViewModel.Result.Subscribe(result =>
                {
                    switch (result)
                    {
                        case MathematicsViewModel.ViewResult.Back:
                            _flowNavigator.Start();
                            break;
                    }
                });
            });
        }
    }
}