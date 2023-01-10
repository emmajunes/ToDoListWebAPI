﻿using Microsoft.AspNetCore.Authorization;
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
        public IActionResult CreateList(string title, string color, Guid userId)
        {
            return Ok(_listService.CreateList(title, color, userId));
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
            var lists = _listService.GetLists();
            return Ok(lists.Where(x => x.UserDtoId == Guid.Parse(userId)));
        }

        [HttpGet("GetList/{id}")]
        public IActionResult GetIndividualList(Guid id)
        {
            return Ok(_listService.GetIndividualList(id));
        }

        [HttpDelete("DeleteList/{id}")]
        public IActionResult Delete(Guid id)
        {
            _listService.DeleteList(id);
            return Ok();
        }

        [HttpPut("EditList/{id}")]
        public IActionResult Put(Guid id, string title)
        {
            return Ok(_listService.EditList(id, title));
        }
    }
}
