using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Tasker.QuartzAdapter.Unity
{
    public static class UnityBootstrap
    {
        static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            return container.LoadConfiguration();
            //Logger.SetLogWriter(new LogWriterFactory().Create());
            //IConfigurationSource config = ConfigurationSourceFactory.Create();
            //ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            //ExceptionManager exceptionManager = factory.CreateManager();
            //ExceptionPolicy.SetExceptionManager(exceptionManager);
            //return container;
        }
    }
}
