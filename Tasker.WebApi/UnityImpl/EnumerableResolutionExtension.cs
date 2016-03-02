using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Tasker.WebApi.UnityImpl
{
    public class EnumerableResolutionExtension : UnityContainerExtension
    {
        /// <summary>
        /// Initial the container with this extension's functionality.
        /// </summary>
        /// <remarks>
        /// When overridden in a derived class, this method will modify the given
        /// <see cref="T:Microsoft.Practices.Unity.ExtensionContext" /> by adding strategies, policies, etc. to
        /// install it's functions into the container.
        /// </remarks>
        protected override void Initialize()
        {
            Context.Strategies.Add(new EnumerableResolutionStrategy(), UnityBuildStage.TypeMapping);
        }
    }
}