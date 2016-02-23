using System;
using Tasker.Common.Abstraction;

namespace Tasker.Common
{
    /// <summary>
    /// TaskBase abstract class'ın dan türeyen örnek Task sınıfı
    /// </summary>
    public class TestTask : TaskBase
    {
        /// <summary>
        /// TestTask sınıfı için oluşturucu metod
        /// </summary>
        public TestTask()
        {
            Init();
            Object o = new object();
        }

        /// <summary>
        /// Varsayılan değerlerin atandığı ve sınıftan bir nesne örneklendiğinde yapılması
        /// gereken işlemlerin implemente edildiği metod
        /// </summary>
        private void Init()
        {
            CronPrefix.Add("0 0 12 1/1 * ? *");
        }

        /// <summary>Modül adı. Modülü tanımlamak için ve diğer modüllerden ayırabilmek için kullanılır</summary>
        public override string ModuleName { get { return "TestModule"; } }

        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        public override string JobName { get { return "TestJob"; } }

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
