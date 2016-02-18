using System.Reflection.Emit;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public static class JobHelper
    {
        public static IJob ConvertItakToIJob<T>(T task)
            where T : ITask
        {
            //var assemblyBuilder = new AssemblyBuilder();
            //var moduleBuilder = new ModuleBuilder(assemblyBuilder,new InternalM);
            //var typeBuilder = new TypeBuilder();


            return null;
        }
    }
}
