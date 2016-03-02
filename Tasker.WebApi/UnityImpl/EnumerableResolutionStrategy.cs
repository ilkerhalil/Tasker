using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Tasker.WebApi.UnityImpl
{
    internal class EnumerableResolutionStrategy : IBuilderStrategy
    {
        /// <summary>
        /// This method replaces the build key used to resolve <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public void PreBuildUp(IBuilderContext context)
        {
            if (context.BuildKey.Type.IsGenericType && context.BuildKey.Type.FullName.StartsWith("System.Collections.Generic.IEnumerable"))
            {
                var arrayType = context.BuildKey.Type.GetGenericArguments()[0].MakeArrayType();
                context.BuildKey = new NamedTypeBuildKey(arrayType, context.BuildKey.Name);
            }
        }

        /// <summary>
        /// This method does nothing.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public void PostBuildUp(IBuilderContext context)
        {
        }

        /// <summary>
        /// This method does nothing.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public void PreTearDown(IBuilderContext context)
        {
        }

        /// <summary>
        /// This method does nothing.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public void PostTearDown(IBuilderContext context)
        {
        }
    }
}