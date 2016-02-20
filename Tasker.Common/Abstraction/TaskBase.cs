using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Todo:Dökümantasyonu yazılmalı
    /// </summary>
    public abstract class TaskBase : ITask
    {
        public abstract string ModuleName { get; }

        public virtual IDictionary<string, object> ModuleParameters { get; }

        protected TaskBase()
        {
            CronPrefix = new List<string>();
            ModuleParameters = new Dictionary<string, object>();
        }

        public virtual IList<string> CronPrefix { get; }

        public abstract string JobName { get; }

        public abstract void Run();

    }
}
