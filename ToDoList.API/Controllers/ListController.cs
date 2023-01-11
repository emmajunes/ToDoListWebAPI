using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Models;
using ToDoList.API.Services;

namespace ToDoList.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpPost("CreateList")]
        public IActionResult CreateList(string title, string color)
        {
            var identity = HttpContext.User.Identity;
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            return Ok(_listService.CreateList(title, color,identity, userId));
        }

        [HttpGet("GetAllLists")]        
        public IActionResult GetLists()
        {            
            return Ok(_listService.GetLists());
        }

        [HttpGet("GetCurrentUserLists")]
        public IActionResult GetCurrentUserLists()
        {
            var identity = HttpContext.User.Identity;
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
     
            return Ok(_listService.GetCurrentUserLists(identity, userId));
        }

        [HttpGet("GetList")]
        public IActionResult GetIndividualList(Guid id)
        {
            return Ok(_listService.GetIndividualList(id));
        }

        [HttpDelete("DeleteList")]
        public IActionResult Delete(Guid id)
        {
            _listService.DeleteList(id);
            return Ok();
        }

        [HttpPut("EditList")]
        public IActionResult Put(Guid id, string title)
        {
            return Ok(_listService.EditList(id, title));
        }

        [HttpPut("EditTitleColor")]
        public IActionResult EditTitleColor(Guid id, string title)
        {
            return Ok(_listService.EditTitleColor(id, title));
        }
    }
}
