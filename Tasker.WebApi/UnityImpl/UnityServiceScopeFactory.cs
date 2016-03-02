using Microsoft.Extensions.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Tasker.WebApi.UnityImpl
{
    public class UnityServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IUnityContainer _container;

        public UnityServiceScopeFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IServiceScope CreateScope()
        {
            return new UnityServiceScope(CreateChildContainer());
        }

        private IUnityContainer CreateChildContainer()
        {
            var child = _container.CreateChildContainer();
            child.AddExtension(new EnumerableResolutionExtension());

            return child;
        }
    }
}