using System.Collections.Generic;
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

        private IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> CreateIJobDetail()
        {
            return Tasks.ToDictionary(di =>
            new JobDetailImpl(di.JobName, di.ImplementIJob().GetType()) as IJobDetail, task => task.CronPrefix.Select(sr => new CronTriggerImpl
            {
                CronExpressionString = sr
            }) as Quartz.Collection.ISet<ITrigger>);
        }

        public void StartTasks()
        {
            var jobDetails = CreateIJobDetail();
            _scheduler.ScheduleJobs(jobDetails, true);
            _scheduler.Start();
        }

        public void StopTask(string taskName)
        {

        }

        public void PauseTask(string taskName)
        {
            throw new System.NotImplementedException();
        }
    }
}