using UI.Views;

namespace UI.Flows
{
    public class GeographyFlow : AbstractFlow
    {
        private readonly UIManager _uiManager;

        public GeographyFlow(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public override void Run()
        {
            _uiManager.Open<GeographyGameView>();
        }
    }
}