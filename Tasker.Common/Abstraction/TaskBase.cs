using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    //<summary>Zamanlanmış görevleri yerine getirecek eklentiler yazabilmek için temel abstract sınıf</summary>
    /// <remarks>
    /// <see cref="Tasker.Common.Abstraction.ITask"/> interface'ini implemente eder. Tasklar ITask
    /// arayüzü doğrudan implemente edilerekte yazılabilirler fakat kod kesişimlerinin tek notada
    /// toplanabilmesi amacı ile Taskların bu sınıftan türetilmesi gerekir
    /// </remarks>
    public abstract class TaskBase : ITask
    {
        /// <summary>Modül adı. Modülü tanımlamak için ve diğer modüllerden ayırabilmek için kullanılır</summary>
        public abstract string ModuleName { get; }

        /// <summary>Modül parametreleri. Modülün çalışmak için ihtiyaç duyabileceği parametrelerin modüle 
        /// aktarılmasını sağlayan özellik.</summary>
        public virtual IDictionary<string, object> ModuleParameters { get; }

        /// <summary>
        /// TaskBase abstract sınıfı için oluşturucu metod
        /// </summary>
        protected TaskBase()
        {
            CronPrefix = new List<string>();
            ModuleParameters = new Dictionary<string, object>();
        }

        /// <summary>
        ///  Görevi tetiklemek için tanımlanacak cron ifadelerini tutan liste
        /// </summary>
        public virtual IList<string> CronPrefix { get; }

        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        public abstract string JobName { get; }

        /// <summary>
        /// Görevi başlatan metod. Bir görev için tanımlanmış olan zamanlayıcı tetiklendiği zaman bu metodu
        /// çağırarak görevi başlatır.
        /// </summary>
        public abstract void Run();

    }
}
