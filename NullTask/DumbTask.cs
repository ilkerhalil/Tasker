using System;
using Tasker.Common.Abstraction;

namespace NullTask
{

    /// <summary>
    /// 
    /// </summary>
    public class DumbTask : TaskBase
    {

        /// <summary>
        /// 
        /// </summary>
        public DumbTask()
        {
            Init();
        }

        private void Init()
        {

            this.TaskTriggerCollection.Add(new TaskTrigger("0 0/1 * 1/1 * ? *"));
            ModuleParameters.Add("test", this);
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
            //ModuleParameters.Add("Exception", new NotImplementedException());
            //TaskSchedulerUnitySpec.Values.Add("Running");
            //TestCollection.CreateTestCollection.ConcurrentBag.Add("Running");
            throw new NotImplementedException();
        }

        public override string Description => "Testler için oluşturulmuş bir tasktır. Her dakida bir tetiklenecek şekilde ayarlanmıştır.";
    }
}
