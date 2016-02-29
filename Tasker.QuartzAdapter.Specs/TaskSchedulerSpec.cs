using System.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Tasker.Common;
using Tasker.Common.Abstraction;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    public class TaskSchedulerSpec
    {
        readonly ITaskScheduler _taskScheduler;
        private readonly IScheduler _scheduler;

        public TaskSchedulerSpec()
        {
            var task = new NullTask();
            task.CronPrefix.Add("	0 0 12 1/1 * ? *");
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            _taskScheduler = new TaskSchedulerImpl(_scheduler, new ITask[]
            {
                task
            });
        }

        [Fact]
        public void TaskArray()
        {
            Assert.True(_taskScheduler.Tasks.Any());

        }

        [Fact]
        public void StartTask()
        {
            _taskScheduler.StartTasks();
            foreach (var jobDetail in ((TaskSchedulerImpl)_taskScheduler).JobDetails)
            {
                foreach (var trigger in jobDetail.Value)
                {
                    Assert.True(_scheduler.GetTriggerState(trigger.Key) == TriggerState.Normal);
                }
            }
        }
        [Fact]
        public void StopTask()
        {
            _taskScheduler.StopTasks();
            foreach (var jobDetail in ((TaskSchedulerImpl)_taskScheduler).JobDetails)
            {

                foreach (var trigger in jobDetail.Value)
                {
                    Assert.True(_scheduler.GetTriggerState(trigger.Key) == TriggerState.None);
                }
            }
        }

        public void PauseTask()
        {
            _taskScheduler.PauseTask("TestJob");

        }
    }
}
