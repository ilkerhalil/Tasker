using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Tasker.QuartzAdapter.Specs
{
    public static class UnityBootstrap
    {
        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            return container;
            //Logger.SetLogWriter(new LogWriterFactory().Create());
            //IConfigurationSource config = ConfigurationSourceFactory.Create();
            //ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            //ExceptionManager exceptionManager = factory.CreateManager();
            //ExceptionPolicy.SetExceptionManager(exceptionManager);
            //return container;
        }
    }
}
