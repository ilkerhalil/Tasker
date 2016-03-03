using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Tasker.Common.Abstraction;
using Tasker.WebApi.Models;

namespace Tasker.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskScheduler _taskScheduler;

        public TaskController(ITaskScheduler taskScheduler)
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
               // CronPrefixs = s.CronPrefixs,
                TaskStatus = TaskStatus.Started
            }).ToList();
        }

    }
}
