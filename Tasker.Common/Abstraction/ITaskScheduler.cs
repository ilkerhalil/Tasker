namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskScheduler
    {
        ITask[] Tasks { get; }
        void PauseTask(string taskName);
        void ShutDown();
        void StartTasks();
        void StopTasks();
    }
}