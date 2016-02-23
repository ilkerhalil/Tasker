﻿using System.Collections.Generic;
using System.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public class TaskSchedulerImpl : IQuartzTaskScheduler
    {
        private readonly IScheduler _scheduler;

        public ITask[] Tasks { get; }

        public TaskSchedulerImpl(IScheduler scheduler, ITask[] tasks)
        {
            _scheduler = scheduler;
            Tasks = tasks;
        }

        public IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> JobDetails
        {
            get
            {
                return Tasks.ToDictionary(task => new JobDetailImpl(task.JobName, task.ImplementIJob().GetType()) as IJobDetail,
                    task => (Quartz.Collection.ISet<ITrigger>)task.CronPrefix.Select(sr => new CronTriggerImpl
                    {
                        CronExpressionString = sr
                    }));
            }
        }

        public void StartTasks()
        {
            _scheduler.ScheduleJobs(JobDetails, true);
            _scheduler.Start();
        }


        public void PauseTask(string taskName)
        {
            var jobkey = this.JobDetails.Keys.Single(w => w.Key.Name == taskName).Key;
            _scheduler.PauseJob(jobkey);
        }

        public void StopTasks()
        {
            _scheduler.Standby();
        }
    }
}