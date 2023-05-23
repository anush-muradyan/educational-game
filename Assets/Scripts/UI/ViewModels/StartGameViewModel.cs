using UniRx;

namespace UI.ViewModels
{
    public class StartGameViewModel : ViewModel
    {
        public enum ViewResult
        {
            None,
            Geography,
            Mathematics
        }

        public ReactiveProperty<ViewResult> Result { get; } = new ReactiveProperty<ViewResult>();

        protected StartGameViewModel()
        {
        }


        public void OnGeographyGameButtonClick()
        {
            Result.SetValueAndForceNotify(ViewResult.Geography);
        }

        public void OnMathematicsGameButtonClick()
        {
            Result.SetValueAndForceNotify(ViewResult.Mathematics);
        }
    }
}