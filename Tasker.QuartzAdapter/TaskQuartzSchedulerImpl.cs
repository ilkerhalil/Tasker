using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public class QuartzTaskSchedulerImpl : IQuartzTaskScheduler
    {
        private readonly IScheduler _scheduler;

        public ITask[] Tasks { get; }

        public QuartzTaskSchedulerImpl(IScheduler scheduler, IJob[] tasks)
        {
            _scheduler = scheduler;
            Tasks = tasks.Select(s => ((TaskDecorator<ITask>)s).TaskIntance).ToArray();
        }

        public IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> JobDetails
        {
            get
            {
                return Tasks.ToDictionary(task =>
                new JobDetailImpl(task.JobName, new TaskDecorator<ITask>(task).GetType())
                {
                    Description = task.Description
                } as IJobDetail,
                    task => new Quartz.Collection.HashSet<ITrigger>(task.TaskTriggerCollection.Select(sr => new CronTriggerImpl
                    {
                        Name = task.JobName,
                        CronExpressionString = sr.CronPrefix
                    })) as Quartz.Collection.ISet<ITrigger>);
            }
        }

        public void StartTasks()
        {
            _scheduler.ScheduleJobs(JobDetails, true);
            _scheduler.Start();
        }


        public void PauseTask(string taskName)
        {
            var jobkey = JobDetails.Keys.Single(w => w.Key.Name == taskName).Key;
            _scheduler.PauseJob(jobkey);
        }

        public void StopTasks()
        {
            _scheduler.Standby();
        }

        public void ShutDown()
        {
            _scheduler.Shutdown(false);

        }
    }
}