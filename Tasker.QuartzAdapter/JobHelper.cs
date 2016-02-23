using System;
using System.Reflection;
using System.Reflection.Emit;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    /// <summary>
    /// JobHelper sınıfı Jobların başlatılması , çalıştırılması gibi yardımcı metodları ve extension metodları
    /// implemente eden yardımcı sınıftır
    /// </summary>
    public static class JobHelper
    {
        /// <summary>
        /// Bu metod aldığı <see cref="Tasker.Common.Abstraction.ITask"/> tipindeki objeden Quartz.IJob arayüzünü
        /// implemente eden bir sınıf oluşturur. Bu sınıf çalışma zamanında dinamik olarak oluşturulur 
        /// ITask arayüzünü implement eden sınıfların Quartz.net tarafından çalıştırılmasını sağlar
        /// </summary>
        /// <param name="task"><see cref="Tasker.Common.Abstraction.ITask"/> interface'ini implement eden bir sınıfa ait obje</param>
        /// <returns><see cref="Quartz.IJob"/> interface'ini implement eden bir sınıfa ait obje</returns>
        public static IJob ImplementIJob<T>(this T task)
            where T : ITask
        {
            var assm = typeof(JobHelper).Assembly.GetName();
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assm,
                AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assm.Name);
            var typeBuilder = moduleBuilder.DefineType(task.JobName, TypeAttributes.Public, task.GetType());
            typeBuilder.AddInterfaceImplementation(typeof(IJob));
            var methodBuilder = typeBuilder.DefineMethod("Execute",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(void),
                new[]
                {
                    typeof(IJobExecutionContext)
                });
            InjectRunMethod(task, methodBuilder);
            typeBuilder.DefineMethodOverride(methodBuilder, typeof(IJob).GetMethod("Execute"));
            var targetType = typeBuilder.CreateType();
            return (IJob)Activator.CreateInstance(targetType);
        }
        /// <summary>
        /// Bu metod dinamik olarak kod enjeksiyonu yapar
        /// </summary>
        /// <typeparam name="T"><see cref="Tasker.Common.Abstraction.ITask"/> yada implementasyonu</typeparam>
        /// <param name="task"><see cref="Tasker.Common.Abstraction.ITask"/> tipini implemente eden bir sınıfa ait obje</param>
        /// <param name="methodBuilder"><see cref="System.Reflection.Emit.MethodBuilder"/> tipinde obje</param>
        private static void InjectRunMethod<T>(T task, MethodBuilder methodBuilder) where T : ITask
        {
            var ilGenerator = methodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, task.GetType().GetMethod("Run"));
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
