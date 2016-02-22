using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public class TaskSchedulerImpl : ITaskScheduler
    {
        public ITask[] Tasks { get; }

        public TaskSchedulerImpl(ITask[] tasks)
        {
            Tasks = tasks;
        }

        public void StarTasks()
        {
            throw new System.NotImplementedException();
        }

        public void StopTask(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public void PauseTask(ITask task)
        {
            throw new System.NotImplementedException();
        }
    }
}