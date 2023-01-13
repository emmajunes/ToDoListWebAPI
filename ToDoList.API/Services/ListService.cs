﻿using Microsoft.AspNetCore.Identity;
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


        public ToDoListDto CreateList(string title, Color color, System.Security.Principal.IIdentity identity, string userId)
        {
            var newList = new ToDoListDto()
            {
                ListDateTime = DateTime.Now.ToString(),
                Id = Guid.NewGuid(),
                ListTitle = title,
                TitleColor = color,
                Tasks = new List<TaskDto>(),
                UserDtoId = Guid.Parse(userId)
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
            CurrentRecord.Id["ListId"] = id.ToString();

            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == id);

            return selectedList;
        }

        public IEnumerable<ToDoListDto> GetCurrentUserLists(System.Security.Principal.IIdentity identity, string userId)
        {       
            //var lists = GetLists();
            //var currentUserLists = lists.Where(x => x.UserDtoId == Guid.Parse(userId));
            var currentUser = _dbContext.User.FirstOrDefault(x => x.Id == Guid.Parse(userId));
            var sortedList = SortLists(currentUser.SortBy, userId);

            return sortedList;
        }

        public ToDoListDto EditTitleColor(Color color)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);

            selectedList.TitleColor = color;

            _dbContext.SaveChanges();

            return selectedList;
        }

        public void DeleteList(Guid? id)
        {
            if(id == null)
            {
                id = Guid.Parse(CurrentRecord.Id["ListId"]);
            }

            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == id);
            _dbContext.ToDoList.Remove(selectedList);
            _dbContext.SaveChanges();

        }

        public ToDoListDto EditList(string title)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);

            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);

            selectedList.ListTitle = title;

            _dbContext.SaveChanges();

            return selectedList;

        }

        public void ChangeSortType(SortList sortAlternative, string userId)
        {
            var currentUser = _dbContext.User.FirstOrDefault(x => x.Id == Guid.Parse(userId));
            currentUser.SortBy = sortAlternative;
            _dbContext.SaveChanges();
        }

        public IEnumerable<ToDoListDto> SortLists(SortList sortAlternative, string userId)
        {
            var lists = _dbContext.ToDoList.ToList();
            var currentUserLists = lists.Where(x => x.UserDtoId == Guid.Parse(userId));

            switch (sortAlternative)
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
