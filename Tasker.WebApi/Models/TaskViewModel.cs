using System.Collections.Generic;

namespace Tasker.WebApi.Models
{
    public class TaskViewModel
    {
        public string TaskName { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public IList<string> CronPrefixs { get; set; }
    }

}

