using UI.Views;
using UI.Views.ViewContainer;
using UnityEngine;

namespace UI.Flows
{
    public class GameFlow:AbstractFlow
    {
        private UIManager _uiManager;

        public GameFlow(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public override void Run()
        {
            _uiManager.Open<StartGameView>();
        }
    }
}