using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Zamanlanmış görevleri yerine getirecek eklentiler yazabilmek için temel abstract sınıf 
    /// <see cref="Tasker.Common.Abstraction.ITask"/> interface'ini implemente eder
    /// </summary>
    public abstract class TaskBase : ITask
    {
        /// <summary>Modül adı. Modülü tanımlamak için ve diğer modüllerden ayırabilmek için kullanılır</summary>
        /// <value><see cref="System.String"/> tipinde bir değer.</value>
        public abstract string ModuleName { get; }

        /// <summary>Modül parametreleri. Modülün çalışmak için ihtiyaç duyabileceği parametrelerin modüle 
        /// aktarılmasını sağlayan özellik.</summary>
        /// <value> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}" /> tipinden bir instance</value>
        public virtual IDictionary<string, object> ModuleParameters { get; }

        /// <summary>
        /// 
        /// </summary>
        protected TaskBase()
        {
            TaskTriggerCollection = new List<TaskTrigger>();
            ModuleParameters = new Dictionary<string, object>();
        }


        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        /// <value><see cref="System.String"/> sınıfından bir değer döndürür</value>
        public abstract string JobName { get; }

        /// <summary>
        /// Görevi başlatan metod. Bir görev için tanımlanmış olan zamanlayıcı tetiklendiği zaman bu metodu
        /// çağırarak görevi başlatır.
        /// </summary>
        public abstract void Run();

        /// <summary>
        ///Todo : Dökümantasyon gerekiyor.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<TaskTrigger> TaskTriggerCollection { get; }

    }
}
