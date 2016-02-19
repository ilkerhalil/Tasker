using System;
using System.CodeDom;
using System.Diagnostics;
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

        public override string ModuleName { get; } = "TestModule";

        public override string JobName { get; } = "TestJob";
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
