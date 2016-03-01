using Quartz;
using Quartz.Core;
using Quartz.Impl;

namespace Tasker.QuartzAdapter.Unity
{
    public class UnitySchedulerFactory : StdSchedulerFactory
    {
        private readonly UnityJobFactory unityJobFactory;

        public UnitySchedulerFactory(UnityJobFactory unityJobFactory)
        {
            this.unityJobFactory = unityJobFactory;
        }

        protected override IScheduler Instantiate(QuartzSchedulerResources rsrcs, QuartzScheduler qs)
        {
            qs.JobFactory = this.unityJobFactory;
            return base.Instantiate(rsrcs, qs);
        }
    }
}