using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Todo:D�k�mantasyon yaz�lmal�
    /// </summary>
    public interface IModule
    {
        string ModuleName { get; }

        IDictionary<string, object> ModuleParameters { get; }
    }
}