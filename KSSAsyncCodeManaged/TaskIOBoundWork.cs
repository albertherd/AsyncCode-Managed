using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSSAsyncCodeManaged
{
    public class TaskIOBoundWork
    {
        public static async void TaskIoBoundWorkMain()
        {
            using (FileStream fileStream = new FileStream(@"f:\f.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    Console.WriteLine("Before reading");
                    string data = await sr.ReadToEndAsync();
                    Console.WriteLine("After reading");
                }
            }
        }
    }


}
