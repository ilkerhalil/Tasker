using System;
using Microsoft.Practices.Unity;

namespace Tasker.WebApi.UnityImpl
{
    public class UnityServiceProvider : IServiceProvider
    {
        private readonly IUnityContainer _container;

        public UnityServiceProvider(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch 
            {
                return null;
            }
        }
    }
}