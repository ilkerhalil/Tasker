using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Todo:Dökümantasyon yazýlmalý
    /// </summary>
    public interface IModule
    {
        string ModuleName { get; }

        IDictionary<string, object> ModuleParameters { get; }
    }
}