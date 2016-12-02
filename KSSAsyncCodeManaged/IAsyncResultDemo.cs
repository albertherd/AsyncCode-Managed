using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSSAsyncCodeManaged
{
    public class IAsyncResultDemo
    {
        private static ManualResetEvent manualEvent = null;

        public static void IAsyncResultDemoMain()
        {
            Console.WriteLine("Randomizing buffer");
            byte[] buffer = new byte[1 << 1];
            new Random().NextBytes(buffer);
            manualEvent = new ManualResetEvent(false);

            FileStream fileStream = new FileStream(@"f:\f.txt", FileMode.OpenOrCreate);
            Console.WriteLine("Started writing to file");
            IAsyncResult res = fileStream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(WriteFinished), fileStream);

            if (res.IsCompleted)
            {
                Console.WriteLine("Code completed before entering callback");
            }
            else
            {
                Console.WriteLine("Created work async.");
            }

            Console.WriteLine("Waiting for write to be done!");
            res.AsyncWaitHandle.WaitOne();
            Console.WriteLine("Done - thread id: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Waiting for callback to close file and such.");
            manualEvent.WaitOne();
            Console.WriteLine("File closed etc");
            Console.Read();
        }

        private static void WriteFinished(IAsyncResult asyncResult)
        {
            Console.WriteLine("Async callback fired on thread id: " + Thread.CurrentThread.ManagedThreadId);
            if (asyncResult.CompletedSynchronously)
            {
                Console.WriteLine("Work was done sync mode");
            }
            else
            {
                Console.WriteLine("Work was done async mode");
            }
            FileStream fileStream = asyncResult.AsyncState as FileStream;
            fileStream.EndWrite(asyncResult);
            fileStream.Dispose();
            manualEvent.Set();
        }
    }
}
