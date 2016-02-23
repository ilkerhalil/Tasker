using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Zamanlanmış görevleri yerine getirecek eklentiler yazabilmek için temel abstract sınıf 
    /// <see cref="Tasker.Common.Abstraction.ITask"/> interface'ini implemente eder
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
