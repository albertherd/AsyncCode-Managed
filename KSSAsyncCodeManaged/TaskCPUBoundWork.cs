using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSSAsyncCodeManaged
{
    public class TaskCPUBoundWork
    {
        private const int TASKS_AMOUNT = 10240;
        private static int tasksFinished = 0;
        private static int tasksActive = 0;
        public static void TaskCPUBoundWorkMain()
        {
            Timer aliveThreadTimer = new Timer(ShowAliveThreadCount, null, 1000, 1000);
            List<Task> tasks = new List<Task>(TASKS_AMOUNT);         
            for(int i = 0; i < TASKS_AMOUNT; i++)
            {
                tasks.Add(Task.Factory.StartNew(DoIntensiveWork));
            }

            do
            {
                int finishedIndex = Task.WaitAny(tasks.ToArray());
                tasks.RemoveAt(finishedIndex);
                Interlocked.Increment(ref tasksFinished);
            }
            while (tasksFinished != TASKS_AMOUNT);
        }

        private static void DoIntensiveWork()
        {
            Interlocked.Increment(ref tasksActive);
            for (int i = 0; i < 6553500; i++) { }          
            Interlocked.Decrement(ref tasksActive);
        }

        private static void ShowAliveThreadCount(object state)
        {
            Console.WriteLine(string.Format("Current active task: {0} Finished Tasks: {1}", tasksActive, tasksFinished));
        }
    }     
}
