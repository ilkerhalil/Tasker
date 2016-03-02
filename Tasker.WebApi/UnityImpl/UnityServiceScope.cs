using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Tasker.WebApi.UnityImpl
{
    public class UnityServiceScope : IServiceScope
    {
        private readonly IUnityContainer _container;
        public IServiceProvider ServiceProvider { get; }

        public UnityServiceScope(IUnityContainer container)
        {
            _container = container;
            ServiceProvider = _container.Resolve<IServiceProvider>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
