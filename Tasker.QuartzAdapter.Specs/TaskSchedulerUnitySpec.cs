using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.Practices.Unity;
using NullTask;
using Tasker.Common.Abstraction;
using Tasker.QuartzAdapter.Unity;
using Xunit;

namespace Tasker.QuartzAdapter.Specs
{
    public class TaskSchedulerUnitySpec
    {

        public static ConcurrentBag<string> Values = new ConcurrentBag<string>();

        private readonly ITaskScheduler _taskScheduler;

        public TaskSchedulerUnitySpec()
        {
            var container = InitContainer().Value;
            _taskScheduler = container.Resolve<ITaskScheduler>();

        }

        [Fact]
        public void TaskArray()
        {
            Assert.True(_taskScheduler.Tasks.Any());
        }

        [Fact]
        public void CronPrefix()
        {
            Assert.NotNull(_taskScheduler.Tasks[0].TaskTriggerCollection);
            Assert.True(_taskScheduler.Tasks[0].TaskTriggerCollection.Count == 1);
        }
        [Fact]
        public void ModuleParameters()
        {
            Assert.NotNull(_taskScheduler.Tasks[0].ModuleParameters);
            Assert.True(_taskScheduler.Tasks[0].ModuleParameters.Count > 0);
        }


        [Fact]
        public void StartContainerTask()
        {
            _taskScheduler.StartTasks();
            var reset = new ManualResetEvent(false);
            reset.WaitOne(60.Seconds());
            Assert.True(TestCollection.CreateTestCollection.ConcurrentBag.Count == 1);
        }


        private static Lazy<IUnityContainer> InitContainer()
        {
            var unityContainer = UnityBootstrap.BuildUnityContainer();
            unityContainer.RegisterType<ITask, NullTask.DumbTask>("NullTask");
            unityContainer.AddNewExtension<TaskUnityExtension>();
            unityContainer.RegisterType<ITaskScheduler, QuartzTaskSchedulerImpl>();
            unityContainer.AddNewExtension<TaskerQuartzUnityExtension>();
            return new Lazy<IUnityContainer>(() => unityContainer);
        }
    }
}
