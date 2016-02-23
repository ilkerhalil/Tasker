using System.Collections.Generic;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    /// <summary>
    /// Task yönetimini sağlayan class'ların yazılabilmesi için implement edilmesi gereken temel interface
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public interface IQuartzTaskScheduler
    {
        /// <summary>
        /// Task listesi
        /// </summary>
        /// <remarks>Çalıştırılacak olan taskların listesi</remarks>
        ITask[] Tasks { get; }
        /// <summary>
        /// Taskları başlat
        /// </summary>
        /// <remarks>Tasks özelliğinde(property) tutulan taskları hepsini başlatır</remarks>
        void StartTasks();
        /// <summary>
        /// Quartz Job detayları (Task ın Quartz.Net eşdeğeri olan tip)
        /// </summary>
        IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> JobDetails { get; }
        /// <summary>
        /// Taskları duraklat
        /// </summary>
        /// <remarks>Çalışan tasklardan bir tanesini duraklatmaya yarayan metod. Geçici olarak
        /// duraklatılmak istenilen task'ın adını parametre olarak alır</remarks>
        /// <param name="taskName">Duraklatılacak olan task'ın adı</param>
        void PauseTask(string taskName);
        /// <summary>
        /// Taskları durdur
        /// </summary>
        /// <remarks>Çalışan taskların hepsini kalıcı olarak durdurur. StartTasks metodu
        /// tekrar çağırılmadan tasklar bir daha çalışmazlar</remarks>
        void StopTasks();
        /// <summary>
        /// Taskları kapat
        /// </summary>
        /// <remarks>Taskları kapatır</remarks>
        void ShutDown();
    }
}
