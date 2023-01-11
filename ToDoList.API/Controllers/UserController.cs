using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateUser(string username, string email, string password)
        {
            return Ok(_userService.CreateUser(username, email, password));
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
        public IActionResult Put(Guid id, string? username, string? email, string? password)
        {
            return Ok(_userService.EditProfile(id, username, email, password));
        }

        [HttpDelete("DeleteUser")]
        public IActionResult Delete(Guid id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
