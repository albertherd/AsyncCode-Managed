using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSSAsyncCodeManaged
{
    public static class ThreadPoolWorkerIoCount
    {
        public static void ThreadPoolWorkerIoCountMain()
        {
            int workerThreads;
            int ioCompletionThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out ioCompletionThreads);
            Console.WriteLine("Worker Threads: {0}, IoCompletionThreads: {1}", workerThreads, ioCompletionThreads);
        }
    }
}
