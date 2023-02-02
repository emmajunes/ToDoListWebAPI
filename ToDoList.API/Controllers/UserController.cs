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
        [HttpPost("Login")]
        public IActionResult Login()
        {
            var user = Request.ReadFromJsonAsync<UserDto>().Result;

            try
            {
                return Ok(_userService.Login(user));
            }
            catch (Exception e) when (e.InnerException is InvalidOperationException)
            {
                return BadRequest("Username and Password is required");
            }
            catch (Exception e) when (e.InnerException is UnauthorizedAccessException)
            {
                return BadRequest("Invalid login");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with creating the token");
            }
        }

        [HttpGet("Logout")]
        public IActionResult LogOut()
        {
            try
            {
                return Ok(_userService.LogOut());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with logging out");
            }
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser()
        {
            try
            {
                var user = Request.ReadFromJsonAsync<UserDto>().Result;
                return Ok(_userService.CreateUser(user));
            }
            catch (Exception)
            {
                return BadRequest("User already exists!");
            }        
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetSingleUser")]
        public IActionResult GetSingleUser()
        {
            try
            {
                Guid userId = Guid.Parse(CurrentRecord.Id["UserId"]);
                return Ok(_userService.GetSingleUser(userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("EditProfile")]
        public IActionResult EditProfile()
        {
            try
            {
                var user = Request.ReadFromJsonAsync<UserDto>().Result;
                Guid userId = Guid.Parse(CurrentRecord.Id["UserId"]);
                return Ok(_userService.EditProfile(userId, user));
            }
            catch (Exception)
            {
                return BadRequest();
            }    
        }

        [HttpPut("PromoteUser")]
        public IActionResult PromoteUser()
        {
            try
            {
                var user = Request.ReadFromJsonAsync<UserDto>().Result;
                return Ok(_userService.PromoteUser(user));
            }
            catch (Exception)
            {
                return BadRequest();
            }         
        }

        [HttpPut("DemoteUser")]
        public IActionResult DemoteUser()
        {
            try
            {
                var user = Request.ReadFromJsonAsync<UserDto>().Result;
                return Ok(_userService.DemoteUser(user));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            try
            {
                Guid userId = Guid.Parse(CurrentRecord.Id["UserId"]);
                return Ok(_userService.DeleteUser(userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("DeleteUserForAdmin")]
        public IActionResult DeleteUserForAdmin()
        {
            try
            {
                var user = Request.ReadFromJsonAsync<UserDto>().Result;
                return Ok(_userService.DeleteUserForAdmin(user));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
