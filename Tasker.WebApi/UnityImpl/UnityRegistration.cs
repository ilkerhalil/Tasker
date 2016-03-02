using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Tasker.WebApi.UnityImpl
{
    public static class UnityRegistration
    {
        public static void Populate(this IUnityContainer container,
            IServiceCollection services)
        {
            container.AddExtension(new EnumerableResolutionExtension());

            container.RegisterInstance(services);
            container.RegisterType<IServiceProvider, UnityServiceProvider>();
            container.RegisterType<IServiceScopeFactory, UnityServiceScopeFactory>();

            foreach (var descriptor in services)
            {
                Register(container, descriptor);
            }
        }

        private static void Register(IUnityContainer container,
            ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationType != null)
            {
                container.RegisterType(descriptor.ServiceType,
                    descriptor.ImplementationType,
                    GetLifetimeManager(descriptor.Lifetime));

                container.RegisterType(descriptor.ServiceType,
                    descriptor.ImplementationType,
                    descriptor.ImplementationType.ToString(),
                    GetLifetimeManager(descriptor.Lifetime));
            }
            else if (descriptor.ImplementationFactory != null)
            {
                container.RegisterType(descriptor.ServiceType,
                    GetLifetimeManager(descriptor.Lifetime),
                    new InjectionFactory(unity =>
                    {
                        var provider = unity.Resolve<IServiceProvider>();
                        return descriptor.ImplementationFactory(provider);
                    }));

                container.RegisterType(descriptor.ServiceType,
                    Guid.NewGuid().ToString(),
                    GetLifetimeManager(descriptor.Lifetime),
                    new InjectionFactory(unity =>
                    {
                        var provider = unity.Resolve<IServiceProvider>();
                        return descriptor.ImplementationFactory(provider);
                    }));
            }
            else if (descriptor.ImplementationInstance != null)
            {
                container.RegisterInstance(descriptor.ServiceType,
                    descriptor.ImplementationInstance,
                    GetLifetimeManager(descriptor.Lifetime));

                container.RegisterInstance(descriptor.ServiceType,
                    Guid.NewGuid().ToString(),
                    descriptor.ImplementationInstance,
                    GetLifetimeManager(descriptor.Lifetime));
            }
        }

        private static LifetimeManager GetLifetimeManager(ServiceLifetime lifecycle)
        {
            switch (lifecycle)
            {
                case ServiceLifetime.Transient:
                    return new TransientLifetimeManager();
                case ServiceLifetime.Singleton:
                    return new ContainerControlledLifetimeManager();
                case ServiceLifetime.Scoped:
                    return new HierarchicalLifetimeManager();
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifecycle), lifecycle, null);
            }

            //return new TransientLifetimeManager();
        }
    }
}