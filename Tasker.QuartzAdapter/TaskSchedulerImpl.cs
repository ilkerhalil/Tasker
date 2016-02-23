using System.Collections.Generic;
using System.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public class TaskSchedulerImpl : IQuartzTaskScheduler
    {
        private readonly IScheduler _scheduler;

        /// <summary>
        /// Task listesi
        /// </summary>
        /// <remarks>Çalıştırılacak olan taskların listesi</remarks>
        public ITask[] Tasks { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="tasks"></param>
        public TaskSchedulerImpl(IScheduler scheduler, ITask[] tasks)
        {
            _scheduler = scheduler;
            Tasks = tasks;
        }

        /// <summary>
        /// Quartz Job detayları (Task ın Quartz.Netteki eşdeğeri olan tip)
        /// </summary>
        public IDictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>> JobDetails
        {
            get
            {
                return Tasks.ToDictionary(task => new JobDetailImpl(task.JobName, task.ImplementIJob().GetType()) as IJobDetail,
                    task => (Quartz.Collection.ISet<ITrigger>)task.CronPrefix.Select(sr => new CronTriggerImpl
                    {
                        CronExpressionString = sr
                    }));
            }
        }

        /// <summary>
        /// Taskları başlat
        /// </summary>
        /// <remarks>Tasks özelliğinde(property) tutulan taskları hepsini başlatır</remarks>
        public void StartTasks()
        {
            _scheduler.ScheduleJobs(JobDetails, true);
            _scheduler.Start();
        }


        /// <summary>
        /// Taskları duraklat
        /// </summary>
        /// <remarks>Çalışan tasklardan bir tanesini duraklatmaya yarayan metod. Geçici olarak
        /// duraklatılmak istenilen task'ın adını parametre olarak alır</remarks>
        /// <param name="taskName">Duraklatılacak olan task'ın adı</param>
        public void PauseTask(string taskName)
        {
            var jobkey = this.JobDetails.Keys.Single(w => w.Key.Name == taskName).Key;
            _scheduler.PauseJob(jobkey);
        }

        /// <summary>
        /// Taskları durdur
        /// </summary>
        /// <remarks>Çalışan taskların hepsini kalıcı olarak durdurur. StartTasks metodu
        /// tekrar çağırılmadan tasklar bir daha çalışmazlar</remarks>
        public void StopTasks()
        {
            _scheduler.Standby();
        }

        /// <summary>
        /// Taskları kapat
        /// </summary>
        /// <remarks>Taskları kapatır</remarks>
        public void ShutDown()
        {
            _scheduler.Shutdown(false);

        }
    }
}