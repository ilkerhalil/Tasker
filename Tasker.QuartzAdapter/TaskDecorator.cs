using System;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public class TaskDecorator<T> : IJob
        where T : class, ITask
    {
        public TaskDecorator(T taskInstance)
        {
            TaskInstance = taskInstance;
        }

        public T TaskInstance { get; }

        public void Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new NotImplementedException();
            }
            if (context.NextFireTimeUtc.HasValue) TaskInstance.NextFireTime = context.NextFireTimeUtc.Value.LocalDateTime;
            if (context.PreviousFireTimeUtc.HasValue) TaskInstance.PreviousFireTime = context.PreviousFireTimeUtc.Value.LocalDateTime;
            TaskInstance.TaskRunTime = context.JobRunTime;
            TaskInstance.Run();
        }
    }
}
