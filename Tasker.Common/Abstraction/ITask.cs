using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Zamanlanmış görev oluşturmak için implemente edilmesi gereken temel interface
    /// </summary>
    public interface ITask:IModule
    {
        /// <summary>
        /// 
        /// </summary>
        IList<string> CronPrefix { get; }
        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        string JobName { get; }

        void Run();

        //void StartTask();

        //void StopTask();

        //void PauseTask();
    }
}
