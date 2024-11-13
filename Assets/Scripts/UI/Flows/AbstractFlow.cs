using System;
using UniRx;

namespace UI.Flows
{
    public interface IFlow {
        void Run();
    }

    public interface IFlowResult<TResult> {
        IObservable<TResult> OnFinish { get; }
        void Finish(TResult result);
    }

    public abstract class AbstractFlow : IFlow
    {
        public IObservable<Unit> OnFinish => _onFinish;
        private Subject<Unit> _onFinish = new();
        public IObservable<Unit> OnCancel => _onCancel;
        private Subject<Unit> _onCancel = new();
        
        public abstract void Run();

        public virtual void Finish() {
            _onFinish?.OnNext(Unit.Default);
        }
		
        public virtual void Cancel() {
            _onCancel?.OnNext(Unit.Default);
        }
    }

    public abstract class AbstractFlow<TResult> : AbstractFlow, IFlowResult<TResult>
    {
        public new IObservable<TResult> OnFinish => _onFinish;
        private Subject<TResult> _onFinish = new();

        public virtual void Finish(TResult result) {
            Finish();
            _onFinish?.OnNext(result);
        }
    }
}