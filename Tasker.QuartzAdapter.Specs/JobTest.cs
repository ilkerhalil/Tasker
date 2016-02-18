using Tasker.Common;
using Tasker.Common.Abstraction;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    public class JobTest
    {
        [Fact]
        public void CreateIJobFromITask()
        {
            var job = JobHelper.ImplementIJob(new TestTask());
            Assert.NotNull(job);
        }
    }


}
