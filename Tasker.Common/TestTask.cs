using System;
using Tasker.Common.Abstraction;

namespace Tasker.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class NullTask : TaskBase
    {

        /// <summary>
        /// 
        /// </summary>
        public NullTask()
        {
            Init();
        }

        private void Init()
        {
            CronPrefix.Add("0 0 12 1/1 * ? *");
        }

        /// <summary>Modül adı. Modülü tanımlamak için ve diğer modüllerden ayırabilmek için kullanılır</summary>
        /// <value><see cref="System.String"/> tipinde bir değer.</value>
        public override string ModuleName { get; } = "TestModule";

        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        /// <value><see cref="System.String"/> sınıfından bir değer döndürür</value>
        public override string JobName { get; } = "TestJob"; 

        /// <summary>
        /// Görevi başlatan metod. Bir görev için tanımlanmış olan zamanlayıcı tetiklendiği zaman bu metodu
        /// çağırarak görevi başlatır.
        /// </summary>
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
