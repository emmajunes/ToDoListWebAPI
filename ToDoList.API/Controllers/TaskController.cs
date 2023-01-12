using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService= taskService;
        }
       
        [HttpPost("AddTask")]
        public IActionResult Post(string taskTitle, string taskDescription, string taskPrio)
        {
            return Ok(_taskService.AddTask(taskTitle, taskDescription, taskPrio));
        }

        [HttpGet("GetAllTasks")]
        public IActionResult GetTasks(Guid listId)
        {
            return Ok(_taskService.GetTasks(listId));
        }

        [HttpGet("GetSingleTask")]
        public IActionResult GetSingleTask(Guid taskId)
        {
            return Ok(_taskService.GetSingleTask(taskId));
        }

        [HttpDelete("DeleteTask")]
        public IActionResult Delete()
        {
            _taskService.DeleteTask();
            return Ok();
        }

        [HttpPut("EditTask")]
        public IActionResult Put(string? title, string? description, string? prio)
        {
            return Ok(_taskService.EditTask(title, description, prio));
        }

        [HttpPut("ToggleTask")]
        public IActionResult ToggleTask(bool completed)
        {
            return Ok(_taskService.ToggleTask(completed));
        }

    }
}
