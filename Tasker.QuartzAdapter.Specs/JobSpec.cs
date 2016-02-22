using System;
using Quartz;
using Tasker.Common;
using Tasker.Common.Abstraction;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    public class JobSpec
    {
        private readonly IJob _job;
        private readonly ITask _task;

        public JobSpec()
        {
            _task = new TestTask();
            _job = _task.ImplementIJob();
        }

        [Fact]
        public void CreateIJobFromITask()
        {
            Assert.NotNull(_job);
        }
        [Fact]
        public void RunExecuteIJob()
        {
            Assert.Throws<NotImplementedException>(() => _job.Execute(null));
        }
    }


}
