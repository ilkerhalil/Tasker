using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Todo:Dökümantasyon yazılmalı
    /// </summary>
    public interface IModule
    {
        string ModuleName { get; }

        IDictionary<string, object> ModuleParameters { get; }
    }
}