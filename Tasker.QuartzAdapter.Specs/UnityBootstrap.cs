using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NullTask;
using Tasker.Common.Abstraction;
using Tasker.QuartzAdapter.Unity;

namespace Tasker.QuartzAdapter.Specs
{
    public static class UnityBootstrap
    {
        public static Lazy<IUnityContainer> BuildUnityContainer()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<ITask, DumbTask>("NullTask", new InjectionConstructor("test"));
            unityContainer.AddNewExtension<TaskUnityExtension>();
            unityContainer.RegisterType<ITaskScheduler, QuartzTaskSchedulerImpl>();
            unityContainer.AddNewExtension<TaskerQuartzUnityExtension>();
            return new Lazy<IUnityContainer>(() => unityContainer);
        }
    }
}
