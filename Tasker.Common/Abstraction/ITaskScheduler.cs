namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskScheduler
    {
        /// <summary>
        /// 
        /// </summary>
        ITask[] Tasks { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskName"></param>
        void PauseTask(string taskName);
        /// <summary>
        /// 
        /// </summary>
        void ShutDown();
        /// <summary>
        /// 
        /// </summary>
        void StartTasks();
        /// <summary>
        /// 
        /// </summary>
        void StopTasks();
    }
}