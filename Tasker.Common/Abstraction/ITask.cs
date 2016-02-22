using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Todo:Dökümantasyon yazılamalı.
    /// </summary>
    public interface ITask:IModule
    {
        IList<string> CronPrefix { get; }

        string JobName { get; }

        void Run();

        //void StartTask();

        //void StopTask();

        //void PauseTask();
    }
}
