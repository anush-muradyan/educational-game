namespace UI.Flows
{
    public class FlowNavigator
    {
        private readonly FlowFactory _flowFactory;

        public FlowNavigator(FlowFactory flowFactory)
        {
            _flowFactory = flowFactory;
        }
        
        public void Start()
        {
            RunGame();
        }

        private AbstractFlow RunGame()
        {
            var flow = _flowFactory.CreateFlow<GameFlow>();
            flow.Run();
            return flow;
        }
    }
}