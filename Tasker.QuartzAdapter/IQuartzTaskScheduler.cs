using System.Collections.Generic;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public interface IQuartzTaskScheduler : ITaskScheduler
    {
        IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> JobDetails { get; }
    }
}
