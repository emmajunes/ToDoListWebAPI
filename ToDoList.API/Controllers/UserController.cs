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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(string username, string email, string password, Access? access)
        {
            return Ok(_userService.CreateUser(username, email, password, access));
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetLists()
        {
            return Ok(_userService.GetUsers());
        }

        
        [HttpGet("GetUser")]
        public IActionResult GetIndividualList(Guid id)
        {
            return Ok(_userService.GetIndividualUser(id));
        }

        [HttpPut("EditProfile")]
        public IActionResult EditProfile(Guid id, string? username, string? email, string? password)
        {
            return Ok(_userService.EditProfile(id, username, email, password));
        }

        [HttpPut("PromoteUser")]
        public IActionResult PromteUser(Guid userId, Access access)
        {
            return Ok(_userService.PromoteUser(userId, access));
        }

        [HttpPut("DemoteUser")]
        public IActionResult DemoteUser(Guid userId, Access access)
        {
            return Ok(_userService.DemoteUser(userId, access));
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(Guid? id)
        {
            if(id == null)
            {
                id = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            }
            
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
