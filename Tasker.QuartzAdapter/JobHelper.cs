using System;
using System.Reflection;
using System.Reflection.Emit;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public static class JobHelper
    {
        public static IJob ImplementIJob<T>(T task)
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
                new[] { typeof(IJobExecutionContext) });
            var ilGenerator = methodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, task.GetType().GetMethod("Run"));
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(methodBuilder, typeof(IJob).GetMethod("Execute"));
            var targetType = typeBuilder.CreateType();
            return (IJob)Activator.CreateInstance(targetType);
        }

    }
}
