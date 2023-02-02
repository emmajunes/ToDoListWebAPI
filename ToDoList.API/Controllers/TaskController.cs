using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Models;
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
        public IActionResult AddTask()
        {
            try
            {
                var task = Request.ReadFromJsonAsync<TaskDto>().Result;
                return Ok(_taskService.AddTask(task));
            }
            catch (Exception)
            {
                return BadRequest();
            }          
        }

        [HttpGet("GetAllTasks")]
        public IActionResult GetTasks()
        {
            try
            {
                return Ok(_taskService.GetTasks());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("GetSingleTask")]
        public IActionResult GetSingleTask()
        {
            try
            {
                Guid taskId = Request.ReadFromJsonAsync<Guid>().Result;
                return Ok(_taskService.GetSingleTask(taskId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask()
        {
            try
            {
                return Ok(_taskService.DeleteTask());
            }
            catch (Exception)
            {
                return BadRequest();
            }        
        }

        [HttpPut("EditTask")]
        public IActionResult EditTask()
        {
            try
            {
                var task = Request.ReadFromJsonAsync<TaskDto>().Result;
                return Ok(_taskService.EditTask(task));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("ToggleTask")]
        public IActionResult ToggleTask()
        {
            try
            {
                var task = Request.ReadFromJsonAsync<TaskDto>().Result;
                return Ok(_taskService.ToggleTask(task));
            }
            catch (Exception)
            {
                return BadRequest();
            }     
        }

        [HttpPut("SortTasks")]
        public IActionResult SortTasks()
        {
            try
            {
                var list = Request.ReadFromJsonAsync<ToDoListDto>().Result;
                _taskService.ChangeSortTypeForTask(list);
                return Ok(_taskService.SortTasks(list));
            }
            catch (Exception)
            {
                return BadRequest();
            }         
        }
    }
}
