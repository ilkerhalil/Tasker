using System;
using Quartz;
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
            _task = new NullTask.DumbTask();
            _job = new TaskDecorator<ITask>(_task);
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
