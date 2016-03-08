using System.Linq;
using Quartz;
using Quartz.Impl;
using Tasker.Common.Abstraction;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    
    public class TaskSchedulerSpec
    {
        private readonly ITaskScheduler _taskScheduler;
        private readonly IScheduler _scheduler;

        public TaskSchedulerSpec()
        {
            var task = new NullTask.DumbTask();
            var taskTrigger = new TaskTrigger("0 0/1 * 1/1 * ? *");
            task.TaskTriggerCollection.Add(taskTrigger);
            var job = new TaskDecorator<ITask>(task);
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            var jobs = new IJob[] { job };
            _taskScheduler = new QuartzTaskSchedulerImpl(_scheduler, jobs);
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
            Assert.True(_taskScheduler.Tasks[0].TaskTriggerCollection.Count > 1);
        }
        [Fact]
        public void ModuleParameters()
        {
            Assert.NotNull(_taskScheduler.Tasks[0].ModuleParameters);
            Assert.True(_taskScheduler.Tasks[0].ModuleParameters.Count > 0);
        }


        [Fact]
        public void StartTask()
        {
            _taskScheduler.StartTasks();
            foreach (var trigger in ((QuartzTaskSchedulerImpl)_taskScheduler).JobDetails.SelectMany(jobDetail => jobDetail.Value))
            {
                Assert.True(_scheduler.GetTriggerState(trigger.Key) == TriggerState.Normal);
            }
           
        }
        [Fact]
        public void PauseTask()
        {
            _taskScheduler.StartTasks();
            _taskScheduler.PauseTask("TestJob");
            foreach (var trigger in ((QuartzTaskSchedulerImpl)_taskScheduler).JobDetails.Where(jobDetail => jobDetail.Key.Key.Name == "TestJob").SelectMany(jobDetail => jobDetail.Value))
            {
                Assert.True(_scheduler.GetTriggerState(trigger.Key) == TriggerState.Paused);
            }
        }
        [Fact]
        public void StopTask()
        {
            _taskScheduler.ShutDown();
            foreach (var trigger in ((QuartzTaskSchedulerImpl)_taskScheduler).JobDetails.SelectMany(jobDetail => jobDetail.Value))
            {
                Assert.Throws<SchedulerException>(()=> _scheduler.GetTriggerState(trigger.Key) == TriggerState.None);
            }
        }
    }
}
