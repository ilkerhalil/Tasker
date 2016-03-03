using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Zamanlanmış görev oluşturmak için implemente edilmesi gereken temel interface
    /// <see cref="Tasker.Common.Abstraction.IModule"/> interface'inden türer
    /// </summary>
    public interface ITask : IModule
    {
      
        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        /// <value><see cref="System.String"/> sınıfından bir değer döndürür</value>
        string JobName { get; }
        /// <summary>
        /// Görevi başlatan metod. Bir görev için tanımlanmış olan zamanlayıcı tetiklendiği zaman bu metodu
        /// çağırarak görevi başlatır.
        /// </summary>
        void Run();

        /// <summary>
        ///Todo : Dökümantasyon gerekiyor.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        IList<TaskTrigger> TaskTriggerCollection { get; }


    }
}
