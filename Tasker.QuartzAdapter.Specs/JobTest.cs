using System;
using Quartz;
using Tasker.Common;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    public class JobSpec
    {
        private IJob job;

        public JobSpec()
        {
            job = JobHelper.ImplementIJob(new TestTask());
        }

        [Fact]
        public void CreateIJobFromITask()
        {
            Assert.NotNull(job);
        }
        [Fact]
        public void RunExecuteIJob()
        {
            Assert.Throws<NotImplementedException>(() => job.Execute(null));
        }
    }


}
