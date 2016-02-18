using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public static class JobHelper
    {
        public static IJob ConvertItakToIJob<T>(T task)
            where T : ITask
        {

            //task.GetType().

            return null;
        }
    }
}
