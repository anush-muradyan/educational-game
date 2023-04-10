using UnityEngine.Events;

namespace UI.Flows
{
    public interface IFlow {
        void Run();
    }

    public interface IFlowResult {
        UnityEvent OnFinish { get; }
        UnityEvent OnCancel { get; }
        void Cancel();
        void Finish();
    }

    public interface IFlowResult<TResult> {
        UnityEvent<TResult> OnFinish { get; }
        void Finish(TResult result);
    }

    public abstract class AbstractFlow : IFlow, IFlowResult {
        public UnityEvent OnFinish { get; }
        public UnityEvent OnCancel { get; }

        protected AbstractFlow() {
            OnFinish = new UnityEvent();
            OnCancel = new UnityEvent();
        }

        public abstract void Run();

        public virtual void Finish() {
            OnFinish?.Invoke();
        }
		
        public virtual void Cancel() {
            OnCancel?.Invoke();
        }
    }

    public abstract class AbstractFlow<TResult> : AbstractFlow, IFlowResult<TResult> {
        public new UnityEvent<TResult> OnFinish { get; }

        protected AbstractFlow() {
            OnFinish = new UnityEvent<TResult>();
        }

        public virtual void Finish(TResult result) {
            Finish();
            OnFinish?.Invoke(result);
        }
    }
}