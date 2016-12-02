using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSSAsyncCodeManaged
{
    class Program
    {
        static void Main(string[] args)
        {
            IAsyncResultDemo.IAsyncResultDemoMain();
            TaskCPUBoundWork.TaskCPUBoundWorkMain();
        }
    }
}
