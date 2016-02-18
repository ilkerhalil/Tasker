using System;
using Tasker.Common.Abstraction;

namespace Tasker.Common
{
    public class TestTask : TaskBase
    {
        public TestTask()
        {

            Init();
        }

        private void Init()
        {
            CronPrefix.Add("0 0 12 1/1 * ? *");
        }

        public override string ModuleName { get; } = "Test_Module";

        public override string JobName { get; } = "Test_Job";
        public override void Run()
        {
            Console.WriteLine("Merhaba");
        }
    }
}
