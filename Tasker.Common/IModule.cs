using System.Collections.Generic;

namespace Tasker.Common
{
    public interface IModule
    {
        string ModuleName { get; }
        IDictionary<string, object> ModuleParameters { get; }
    }
}