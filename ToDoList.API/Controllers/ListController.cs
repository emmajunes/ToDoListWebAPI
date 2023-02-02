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
        private readonly IUserService _userService;

        public ListController(IListService listService, IUserService userService)
        {
            _listService = listService;
            _userService = userService;
        }

        [HttpPost("CreateList")]
        public IActionResult CreateList()
        {
            try
            {
                var list = Request.ReadFromJsonAsync<ToDoListDto>().Result;
                Guid userId = Guid.Parse(CurrentRecord.Id["UserId"]);
                return Ok(_listService.CreateList(list, userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllLists")]        
        public IActionResult GetLists()
        {
            try
            {
                return Ok(_listService.GetLists());
            }
            catch (Exception)
            {
                return BadRequest();
            }           
        }

        [HttpGet("GetCurrentUserLists")]
        public IActionResult GetCurrentUserLists()
        {
            try
            {
                Guid userId = Guid.Parse(CurrentRecord.Id["UserId"]);
                return Ok(_listService.GetCurrentUserLists(userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("GetSingleList")]
        public IActionResult GetSingleList()
        {
            try
            {
                Guid listId = Request.ReadFromJsonAsync<Guid>().Result;
                return Ok(_listService.GetSingleList(listId));
            }
            catch (Exception)
            {
                return BadRequest();
            }         
        }

        [HttpDelete("DeleteList")]
        public IActionResult Delete()
        {
            try
            {
                return Ok(_listService.DeleteList());
            }
            catch (Exception)
            {
                return BadRequest();
            }          
        }

        [HttpPut("EditList")]
        public IActionResult EditList()
        {
            try
            {
                var list = Request.ReadFromJsonAsync<ToDoListDto>().Result;
                return Ok(_listService.EditList(list));
            }
            catch (Exception)
            {
                return BadRequest();
            }         
        }

        [HttpPut("EditTitleColor")]
        public IActionResult EditTitleColor()
        {
            try
            {
                var list = Request.ReadFromJsonAsync<ToDoListDto>().Result;
                return Ok(_listService.EditTitleColor(list));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("SortList")]
        public IActionResult SortLists()
        {
            try
            {
                var user = Request.ReadFromJsonAsync<UserDto>().Result;
                _userService.ChangeSortType(user);
                return Ok(_listService.SortLists(user));
            }
            catch (Exception)
            {
                return BadRequest();
            }        
        }
    }
}
