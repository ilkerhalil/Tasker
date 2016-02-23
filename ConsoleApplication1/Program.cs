using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Common.Abstraction;
using Tasker.Common;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskBase tb;
            TestTask tt;
        }
    }
    public class TestImp : ITask
    {
        public void Run() { }    
    }

}
