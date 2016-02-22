using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public interface IQuartzTaskScheduler
    {
        ITask[] Tasks { get; }

        void StartTasks();

        void StopTask(string taskName);

        void PauseTask(string taskName);

    }
}
