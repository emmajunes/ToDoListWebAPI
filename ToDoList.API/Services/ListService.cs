using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public class ListService : IListService
    {
        private readonly ToDoListContext _dbContext;
        public ListService(ToDoListContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public ToDoListDto CreateList(string title, string color, Guid userId)
        {
            var newList = new ToDoListDto()
            {
                ListDateTime = DateTime.Now.ToString(),
                Id = Guid.NewGuid(),
                ListTitle = title,
                TitleColor = color,
                Tasks = new List<TaskDto>(),
                UserDtoId = userId //koppla till inloggad person
            };

            _dbContext.ToDoList.Add(newList);
            _dbContext.SaveChanges();

            return newList;

        }

        public IEnumerable<ToDoListDto> GetLists()
        {
            return _dbContext.ToDoList.ToList();
        }

        public ToDoListDto GetIndividualList(Guid id)
        {
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == id);

            return selectedList;
        }

        public IEnumerable<ToDoListDto> GetCurrentUserLists(System.Security.Principal.IIdentity identity, string userId)
        {       
            var lists = GetLists();
            return lists.Where(x => x.UserDtoId == Guid.Parse(userId));
        }

        private void ColorList(ToDoListDto todoList, string color)
        {
            todoList.TitleColor = color;
        }

        public void DeleteList(Guid id)
        {
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == id);
            _dbContext.ToDoList.Remove(selectedList);
            _dbContext.SaveChanges();

        }

        public ToDoListDto EditList(Guid id, string title)
        {

            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == id);

            selectedList.ListTitle = title;

            _dbContext.SaveChanges();

            return selectedList;

        }

    }
}
