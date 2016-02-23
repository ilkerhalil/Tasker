using System.Collections.Generic;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public interface IQuartzTaskScheduler
    {
        ITask[] Tasks { get; }

        void StartTasks();

        IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> JobDetails { get; }

        void PauseTask(string taskName);

        void StopTasks();
    }
}
