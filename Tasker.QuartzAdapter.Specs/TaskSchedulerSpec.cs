﻿using System.Linq;
using Quartz;
using Quartz.Impl;
using Tasker.Common;
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
            var task = new NullTask.NullTask();
            task.CronPrefix.Add("0 0 12 1/1 * ? *3");
            var job = task.ImplementIJob();
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
            Assert.NotNull(_taskScheduler.Tasks[0].CronPrefix);
            Assert.True(_taskScheduler.Tasks[0].CronPrefix.Count > 1);
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
        public void StopTask()
        {
            _taskScheduler.StopTasks();
            foreach (var trigger in ((QuartzTaskSchedulerImpl)_taskScheduler).JobDetails.SelectMany(jobDetail => jobDetail.Value))
            {
                Assert.True(_scheduler.GetTriggerState(trigger.Key) == TriggerState.None);
            }
        }

        public void PauseTask()
        {
            _taskScheduler.PauseTask("TestJob");
            foreach (var trigger in ((QuartzTaskSchedulerImpl)_taskScheduler).JobDetails.Where(jobDetail => jobDetail.Key.Key.Name == "TestJob").SelectMany(jobDetail => jobDetail.Value))
            {
                Assert.True(_scheduler.GetTriggerState(trigger.Key) == TriggerState.Paused);
            }
        }
    }
}
