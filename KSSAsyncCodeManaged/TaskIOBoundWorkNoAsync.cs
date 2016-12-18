using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KSSAsyncCodeManaged
{
    public class TaskIOBoundWorkNoAsync
	{
		private sealed class TaskIoBoundWorkMain : IAsyncStateMachine
		{
            public int state;

			public AsyncVoidMethodBuilder builder;

			private FileStream fileStream;

			private StreamReader streamReader;

			private string data;

			private string compilerGeneratedString;

			private TaskAwaiter<string> taskAwaiter;

			void IAsyncStateMachine.MoveNext()
			{
                int num = this.state;
				try
				{
					if (num != 0)
					{
						this.fileStream = new FileStream("f:\\f.txt", FileMode.OpenOrCreate);
					}
					try
					{
						if (num != 0)
						{
							this.streamReader = new StreamReader(this.fileStream);
						}
						try
						{
							TaskAwaiter<string> taskAwaiter;
							if (num != 0)
							{
								Console.WriteLine("Before reading");
								taskAwaiter = this.streamReader.ReadToEndAsync().GetAwaiter();
								if (!taskAwaiter.IsCompleted)
								{
									num = (this.state = 0);
									this.taskAwaiter = taskAwaiter;
                                    TaskIOBoundWorkNoAsync.TaskIoBoundWorkMain stateMachine = this;
									this.builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, TaskIOBoundWorkNoAsync.TaskIoBoundWorkMain>(ref taskAwaiter, ref stateMachine);
									return;
								}
							}
							else
							{
								taskAwaiter = this.taskAwaiter;
								this.taskAwaiter = default(TaskAwaiter<string>);
								num = (this.state = -1);
							}
							string result = taskAwaiter.GetResult();
							taskAwaiter = default(TaskAwaiter<string>);
							this.compilerGeneratedString = result;
							this.data = this.compilerGeneratedString;
							this.compilerGeneratedString = null;
							Console.WriteLine("After reading");
							this.data = null;
						}
						finally
						{
							if (num < 0 && this.streamReader != null)
							{
								((IDisposable)this.streamReader).Dispose();
							}
						}
						this.streamReader = null;
					}
					finally
					{
						if (num < 0 && this.fileStream != null)
						{
							((IDisposable)this.fileStream).Dispose();
						}
					}
					this.fileStream = null;
				}
				catch (Exception exception)
				{
					this.state = -2;
					this.builder.SetException(exception);
					return;
				}
				this.state = -2;
				this.builder.SetResult();
			}


			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}


		public static void TaskIOBoundWorkNoAsyncMain()
		{
            TaskIOBoundWorkNoAsync.TaskIoBoundWorkMain stateMachine = new TaskIOBoundWorkNoAsync.TaskIoBoundWorkMain();
			stateMachine.builder = AsyncVoidMethodBuilder.Create();
			stateMachine.state = -1;
			AsyncVoidMethodBuilder builder = stateMachine.builder;
			builder.Start<TaskIOBoundWorkNoAsync.TaskIoBoundWorkMain>(ref stateMachine);
		}
	}
}
