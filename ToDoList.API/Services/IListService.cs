using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IListService
    {
        ToDoListDto CreateList(ToDoListDto item);
        IEnumerable<ToDoListDto> GetLists();

        void DeleteList(ToDoListDto item);
    }
}