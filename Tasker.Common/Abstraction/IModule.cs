using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    public interface IModule
    {
        string ModuleName { get; }

        IDictionary<string, object> ModuleParameters { get; }
    }
}