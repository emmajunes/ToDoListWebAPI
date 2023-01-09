using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Models;
using ToDoList.API.Services;

namespace ToDoList.API.Controllers
{
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
            //var item = new ToDoListDto
            //{
            //    ListTitle = title,
            //    TitleColor = color
            //};

            return Ok(_listService.CreateList(title, color));
        }

        [HttpGet("GetLists")]
        public IActionResult GetLists()
        {
            return Ok(_listService.GetLists());
        }

        [HttpGet("GetList/{id}")]
        public IActionResult GetIndividualList(Guid id)
        {
            return Ok(_listService.GetIndividualList(id));
        }

        [HttpDelete("DeleteList/{id}")]
        public IActionResult Delete(Guid id)
        {
            //var item = new ToDoListDto
            //{
            //    ListId = id
            //};

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
