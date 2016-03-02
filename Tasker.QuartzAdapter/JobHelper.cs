using System;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Practices.ObjectBuilder2;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    /// <summary>
    /// 
    /// </summary>
    public static class JobHelper
    {
        /// <summary>
        /// <see cref="ITask"/> interface'ini implemente eden bir class'a ait bir instance parametre olarak verildiğinde 
        /// parametre olarak verilen obje ye dinamik olarak <see cref="IJob"/> interface'ini implemente eder. dönüşte IJob
        /// türünde bir obje olarak geri döndürür.
        /// </summary>
        /// <typeparam name="T"><see cref="ITask"/> interface yada ITask' tan türeyen bir tip</typeparam>
        /// <param name="task">T type parametresinde belirtilmiş olan tip'e ait bir instance</param>
        /// <returns>IJob tipine ait bir instance</returns>
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
            return MapProperties(task,(IJob) Activator.CreateInstance(targetType));
        }

        /// <summary>
        /// Girişte verilen ITask türündeki obje üzerindeki property'leri IJob türünde olan objeye map eder. 
        /// taşıdıkları değerleri eşitler
        /// </summary>
        /// <param name="sourceInstance"><see cref="ITask"/> türünde bir instance</param>
        /// <param name="targetInstance"><see cref="IJob"/> türünde bir instance</param>
        /// <returns>Girişte verilen IJob türündeki instance geriye döner</returns>
        private static IJob MapProperties(ITask sourceInstance, IJob targetInstance)
        {
            PropertyInfo[] sourceInstanceProperties = sourceInstance.GetType().GetProperties();
            sourceInstanceProperties.ForEach(propertyInfo =>
            {
                var sourceInstancePropertyValue = propertyInfo.GetValue(sourceInstance);
                if (sourceInstancePropertyValue != null)
                {
                    var backingFieldName = string.Format("<{0}>k__BackingField", propertyInfo.Name);
                    var backingField = propertyInfo.DeclaringType.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                    backingField.SetValue(targetInstance, sourceInstancePropertyValue);
                }
            });
            return targetInstance;
        }
        /// <summary>
        /// ITask türündeki objeye "Run" ismindeki metodu enjekte eder.
        /// </summary>
        /// <typeparam name="T">ITask interface'ini implemente eden bir tip</typeparam>
        /// <param name="task"></param>
        /// <param name="methodBuilder"></param>
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
