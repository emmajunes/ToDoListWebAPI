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
        public IActionResult Post(Guid listId, string taskTitle, string taskDescription, string taskPrio)
        {
            return Ok(_taskService.AddTask(listId, taskTitle, taskDescription, taskPrio));
        }

        [HttpGet("GetAllTasks")]
        public IActionResult GetTasks(Guid listId)
        {
            return Ok(_taskService.GetTasks(listId));
        }

        [HttpGet("GetTask")]
        public IActionResult GetIndividualTask(Guid taskId)
        {
            return Ok(_taskService.GetIndividualTask(taskId));
        }

        [HttpDelete("DeleteTask")]
        public IActionResult Delete(Guid taskId)
        {
            _taskService.DeleteTask(taskId);
            return Ok();
        }

        [HttpPut("EditTask")]
        public IActionResult Put(Guid taskId, string title, string description, string prio)
        {
            return Ok(_taskService.EditTask(taskId, title, description, prio));
        }

    }
}
