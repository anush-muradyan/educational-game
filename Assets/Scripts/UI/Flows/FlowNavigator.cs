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

        public AbstractFlow RunGeographyFlow()
        {
            var flow = _flowFactory.CreateFlow<GeographyFlow>();
            flow.Run();
            return flow;
        }

        public AbstractFlow RunMathematicsFlow()
        {
            var flow = _flowFactory.CreateFlow<MathematicsFlow>();
            flow.Run();
            return flow;
        }
    }
}