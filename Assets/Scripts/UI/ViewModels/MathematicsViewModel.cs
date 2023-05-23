using UniRx;

namespace UI.ViewModels
{
    public class MathematicsViewModel : ViewModel
    {   
        public enum ViewResult
        {
            None,
            Back
        }

        public ReactiveProperty<ViewResult> Result { get; } = new ReactiveProperty<ViewResult>();
        
        public void OnBackButtonClick()
        {
            Result.SetValueAndForceNotify(ViewResult.Back);
        }
    }
}