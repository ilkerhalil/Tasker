using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Tasker.Common.Abstraction;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    public class TaskSchedulerUnitySpec
    {
        private readonly ITaskScheduler _taskScheduler;

        public TaskSchedulerUnitySpec()
        {
            var container = UnityBootstrap.BuildUnityContainer().Value;
            _taskScheduler = container.Resolve<ITaskScheduler>();
        }

        [Fact]
        public void TaskArray()
        {
            Assert.True(_taskScheduler.Tasks.Any());
        }

        [Fact]
        public void CronPrefix()
        {
            Assert.NotNull(_taskScheduler.Tasks[0].TaskTriggerCollection);
            Assert.True(_taskScheduler.Tasks[0].TaskTriggerCollection.Count == 1);
        }
        [Fact]
        public void ModuleParameters()
        {
            Assert.NotNull(_taskScheduler.Tasks[0].ModuleParameters);
            Assert.True(_taskScheduler.Tasks[0].ModuleParameters.Count > 0);
        }

        [Fact]
        public void StartContainerTask()
        {
            _taskScheduler.StartTasks();
            var reset = new ManualResetEvent(false);
            reset.WaitOne(60.Seconds());
            var nextFireTime = _taskScheduler.Tasks.First().NextFireTime;
            Assert.NotEqual(nextFireTime.ToUniversalTime(),default(DateTime).ToUniversalTime());
        }
    }
}
