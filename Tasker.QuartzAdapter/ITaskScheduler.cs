using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public interface ITaskScheduler
    {
        ITask[] Tasks { get; }

        void StarTasks();

        void StopTask(ITask task);

        void PauseTask(ITask task);

    }
}
