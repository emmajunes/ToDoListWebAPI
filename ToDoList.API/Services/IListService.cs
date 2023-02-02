using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IListService
    {
        ToDoListDto CreateList(ToDoListDto list, Guid userId);
        IEnumerable<ToDoListDto> GetLists();
        ToDoListDto GetSingleList(Guid id);
        IEnumerable<ToDoListDto> GetCurrentUserLists(Guid userId);
        ToDoListDto DeleteList();
        ToDoListDto EditList(ToDoListDto list);
        ToDoListDto EditTitleColor(ToDoListDto list);
        IEnumerable<ToDoListDto> SortLists(UserDto user);
    }
}