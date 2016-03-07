using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public class TaskDecorator<T> : IJob
        where T : class, ITask
    {
        public TaskDecorator(T taskIntance)
        {
            TaskIntance = taskIntance;
        }

        public T TaskIntance { get; }

        public void Execute(IJobExecutionContext context)
        {
            if (context == null) return;
            if (context.NextFireTimeUtc.HasValue) TaskIntance.NextFireTime = context.NextFireTimeUtc.Value.LocalDateTime;
            if (context.PreviousFireTimeUtc.HasValue) TaskIntance.PreviousFireTime = context.PreviousFireTimeUtc.Value.LocalDateTime;
            TaskIntance.TaskRunTime = context.JobRunTime;
            TaskIntance.Run();
        }
    }
}
