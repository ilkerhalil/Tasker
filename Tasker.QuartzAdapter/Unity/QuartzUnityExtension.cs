using Microsoft.Practices.Unity;
using Quartz;

namespace Tasker.QuartzAdapter.Unity
{
    public class TaskerQuartzUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ISchedulerFactory, UnitySchedulerFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IScheduler>(new InjectionFactory(c => c.Resolve<ISchedulerFactory>().GetScheduler()));
        }
    }
}
