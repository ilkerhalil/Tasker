using Quartz;
using Quartz.Core;
using Quartz.Impl;

namespace Tasker.QuartzAdapter.Unity
{
    public class UnitySchedulerFactory : StdSchedulerFactory
    {
        private readonly UnityJobFactory _unityJobFactory;

        public UnitySchedulerFactory(UnityJobFactory unityJobFactory)
        {
            _unityJobFactory = unityJobFactory;
        }

        protected override IScheduler Instantiate(QuartzSchedulerResources rsrcs, QuartzScheduler qs)
        {
            qs.JobFactory = _unityJobFactory;
            return base.Instantiate(rsrcs, qs);
        }
    }
}