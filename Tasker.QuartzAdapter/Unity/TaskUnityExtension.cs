using System.Linq;
using Microsoft.Practices.Unity;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter.Unity
{
    public class TaskUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var tasks = Container.ResolveAll<ITask>().ToArray();
            if (!tasks.Any()) return;
            foreach (var task in tasks)
            {
                Container.RegisterType<IJob>(task.JobName, new InjectionFactory(c => task.ImplementIJob()));
            }
        }
    }
}
