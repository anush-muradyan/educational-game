using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Core.Shared
{
    public class AsyncTask: CustomYieldInstruction, IDisposable {
		public static AsyncTask<List<AsyncTask>> All(params AsyncTask[] tasks) {
			return new CombinedAsyncTask(new List<AsyncTask>(tasks));
		}

		public delegate void TaskSuccessDelegate();

		public delegate void TaskFailDelegate(Exception error);

		public delegate void TaskCompleteDelegate(Exception error);

		public bool IsDone { get; private set; }
		public Exception Error { get; private set; }

		private TaskSuccessDelegate onSuccess;
		private TaskFailDelegate onFail;
		private TaskCompleteDelegate onComplete;
		private bool _keepWaiting = true;

		public AsyncTask OnSuccessOnce(TaskSuccessDelegate onSuccess) {
			TaskSuccessDelegate d = null;
			d = () => {
				this.onSuccess -= d;
				onSuccess();
			};
			this.onSuccess += d;
			if (IsDone && Error == null && onSuccess != null) {
				d.Invoke();
			}

			return this;
		}

		public AsyncTask OnSuccess(TaskSuccessDelegate onSuccess) {
			this.onSuccess += onSuccess;
			if (IsDone && Error == null && onSuccess != null) {
				onSuccess.Invoke();
			}

			return this;
		}

		public AsyncTask OnFailOnce(TaskFailDelegate onFail) {
			TaskFailDelegate d = null;

			d = error => {
				this.onFail -= d;
				onFail(error);
			};

			this.onFail += d;
			if (IsDone && Error != null && onFail != null) {
				d.Invoke(Error);
			}

			return this;
		}

		public AsyncTask OnFail(TaskFailDelegate onFail) { 
			this.onFail += onFail;
			if (IsDone && Error != null && onFail != null) {
				onFail.Invoke(Error);
			}

			return this;
		}

		public AsyncTask OnComplete(TaskCompleteDelegate onComplete) {
			this.onComplete += onComplete;
			if (IsDone && onComplete != null) {
				onComplete.Invoke(Error);
			}

			return this;
		}

		public virtual void Success() {
			IsDone = true;

			if (onSuccess != null) {
				onSuccess();
			}

			if (onComplete != null) {
				onComplete(null);
			}

			_keepWaiting = false;
		}

		public void Fail(Exception error) {
			Error = error;
			IsDone = true;

			if (onFail != null) {
				onFail(error);
			}

			if (onComplete != null) {
				onComplete(error);
			}

			_keepWaiting = false;
		}

		public override bool keepWaiting {
			get { return _keepWaiting; }
		}


		public virtual void Dispose() {
			onSuccess = null;
			onComplete = null;
			onFail = null;
		}
	}

	public class AsyncTask<T> : AsyncTask {
		public new delegate void TaskSuccessDelegate(T result);
		public new delegate void TaskFailDelegate(T result,Exception exception);

		public new delegate void TaskCompleteDelegate(T result, Exception error);

		public T Result { get; private set; }

		private TaskSuccessDelegate onSuccess;
		private TaskFailDelegate onFail;
		private TaskSuccessDelegate onSuccessOnce;
		private TaskCompleteDelegate onComplete;

		public AsyncTask<T> OnSuccessOnce(TaskSuccessDelegate onSuccess) {
			TaskSuccessDelegate d = null;
			d = (r) => {
				this.onSuccess -= d;
				onSuccess(r);
			};
			this.onSuccess += d;
			if (IsDone && Error == null && onSuccess != null) {
				d.Invoke(Result);
			}

			return this;
		}

		public AsyncTask<T> OnSuccess(TaskSuccessDelegate onSuccess) {
			this.onSuccess += onSuccess;
			if (IsDone && Error == null && onSuccess != null) {
				onSuccess.Invoke(Result);
			}

			return this;
		}

		public AsyncTask<T> OnComplete(TaskCompleteDelegate onComplete) {
			this.onComplete += onComplete;
			if (IsDone && onComplete != null) {
				onComplete.Invoke(Result, Error);
			}

			return this;
		}

		public void Success(T result) {
			Result = result;
			base.Success();

			if (onSuccess != null) {
				onSuccess(result);
			}

			if (onComplete != null) {
				onComplete(result, null);
			}
		}

		public new void Fail(Exception exception)
		{
			base.Fail(exception);
			if (onFail != null)
			{
				onFail(Result, exception);
			}

			if (onComplete!=null)
			{
				onComplete(Result, exception);
			}
		}
		

		public override void Success() {
			throw new ArgumentException("Calling Success without result on AsyncTask that expects " + typeof(T));
		}

		public override void Dispose() {
			base.Dispose();

			onSuccess = null;
			onComplete = null;
		}
	}

	internal class CombinedAsyncTask : AsyncTask<List<AsyncTask>> {
		private List<AsyncTask> tasks;
		private List<AsyncTask> activeTasks;
		private bool failed;
		private Exception error;

		public CombinedAsyncTask(List<AsyncTask> tasks) {
			this.tasks = tasks;
			activeTasks = new List<AsyncTask>(tasks);
			listenAllSuccess();
		}

		private void listenAllSuccess() {
			List<AsyncTask> tempTasks = new List<AsyncTask>(tasks);
			foreach (var task in tempTasks) {
				AsyncTask tempTask = task;
				tempTask
					.OnSuccess(delegate {
						activeTasks.Remove(tempTask);
						checkComplete();
					})
					.OnFail(delegate(Exception error) {
						activeTasks.Remove(tempTask);
						failed = true;
						this.error = error;
						checkComplete();
					});
			}
		}

		private void checkComplete() {
			if (activeTasks.Count > 0) {
				return;
			}

			if (failed) {
				Fail(error);
			}
			else {
				Success(tasks);
			}
		}
	}

	public static class AsyncTaskExtensions {
		public static T Result<T>(this AsyncTask task) {
			if (task is AsyncTask<T> genericTask) {
				return genericTask.Result;
			}

			return default;
		}

		public static T Result<T>(this List<AsyncTask> tasks) {
			var index = tasks.FindIndex(t => t is AsyncTask<T>);
			return index < 0 ? default : tasks[index].Result<T>();
		}
	}
}