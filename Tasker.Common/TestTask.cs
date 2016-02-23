using System;
using Tasker.Common.Abstraction;

namespace Tasker.Common
{
    public class TestTask : TaskBase
    {
        public TestTask()
        {
            Init();
            Object o = new object();
        }

        private void Init()
        {
            CronPrefix.Add("0 0 12 1/1 * ? *");
        }

        public override string ModuleName { get { return "TestModule"; } }

        public override string JobName { get { return "TestJob"; } }
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
