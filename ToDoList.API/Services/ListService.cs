using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoList.API.Models;
using System.Threading.Tasks;

namespace ToDoList.API.Services
{
    public class ListService : IListService
    {
        private readonly ToDoListContext _dbContext;
        public ListService(ToDoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ToDoListDto CreateList(ToDoListDto list, Guid userId)
        {
            var newList = new ToDoListDto()
            {
                ListDateTime = DateTime.Now.ToString(),
                Id = Guid.NewGuid(),
                ListTitle = list.ListTitle,
                TitleColor = list.TitleColor,
                Tasks = new List<TaskDto>(),
                UserDtoId = userId
            };

            _dbContext.ToDoList.Add(newList);
            _dbContext.SaveChanges();

            return newList;
        }

        public IEnumerable<ToDoListDto> GetLists()
        {
            return _dbContext.ToDoList.ToList();
        }

        public ToDoListDto GetSingleList(Guid listId)
        {
            CurrentRecord.Id["ListId"] = listId.ToString();
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);

            return selectedList;
        }
        public IEnumerable<ToDoListDto> GetCurrentUserLists(Guid userId)
        {       
            var currentUser = _dbContext.User.FirstOrDefault(x => x.Id == userId);
            var sortedList = SortLists(currentUser); 

            return sortedList;
        }

        public ToDoListDto EditTitleColor(ToDoListDto list)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);
            selectedList.TitleColor = list.TitleColor;
            _dbContext.SaveChanges();

            return selectedList;
        }
        public ToDoListDto DeleteList()
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);
            _dbContext.ToDoList.Remove(selectedList);
            _dbContext.SaveChanges();

            return selectedList;
        }
        public ToDoListDto EditList(ToDoListDto list)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);
            selectedList.ListTitle = list.ListTitle == null ? selectedList.ListTitle : list.ListTitle;
            _dbContext.SaveChanges();

            return selectedList;

        }
        public IEnumerable<ToDoListDto> SortLists(UserDto user)
        {
            var lists = _dbContext.ToDoList.ToList();
            var currentUserLists = lists.Where(x => x.UserDtoId == user.Id);

            switch (user.SortBy)
            {
                case SortList.Ascendning:
                    currentUserLists = currentUserLists.OrderBy(x => x.ListDateTime).ToList();
                    break;
                case SortList.Descending:
                    currentUserLists = currentUserLists.OrderByDescending(x => x.ListDateTime).ToList();
                    break;
                case SortList.Alphabetic:
                    currentUserLists = currentUserLists.OrderBy(x => x.ListTitle).ToList();
                    break;
                case SortList.Color:
                    currentUserLists = currentUserLists.OrderBy(x => x.TitleColor).ToList();
                    break;
            }

            return currentUserLists;
        }

    }
}
