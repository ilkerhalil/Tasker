using System;
using Tasker.Common.Abstraction;

namespace Tasker.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class NullTask : TaskBase
    {

        /// <summary>
        /// 
        /// </summary>
        public NullTask()
        {
            Init();
        }

        private void Init()
        {
            CronPrefix.Add("0 0 12 1/1 * ? *");
        }

        public override string ModuleName { get; } = "TestModule";

        public override string JobName { get { return "TestJob"; } }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
