using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Tasker.Common.Abstraction;
using Tasker.WebApi.Models;

namespace Tasker.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly ITaskScheduler _taskScheduler;

        public TasksController(ITaskScheduler taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TaskViewModel> Get()
        {
            return _taskScheduler.Tasks.Select(s => new TaskViewModel
            {
                TaskName = s.JobName,
                CronPrefixs = s.TaskTriggerCollection.Select(st => st.CronPrefix).ToList(),
                TaskStatus = TaskStatus.Started
            }).ToList();
        }

    }
}
