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
        private ToDoListContext _dbContext;

        public ListController(IListService listService, ToDoListContext dbContext)
        {
            _listService = listService;
            _dbContext = dbContext;
        }

        [HttpPost("CreateList")]
        public IActionResult CreateList(string title, string color)
        {
            var item = new ToDoListDto
            {
                ListTitle = title,
                TitleColor = color
            };

            return Ok(_listService.CreateList(item));
        }

        [HttpGet]
        public IEnumerable<ToDoListDto> Get()
        {
            return (_dbContext.ToDoList.Include(x => x.ListTitle).ToList());
        }
    }
}
