using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IListService
    {
        //ToDoListDto CreateList(string title, Color color, System.Security.Principal.IIdentity identity, string userId);

        ToDoListDto CreateList(ToDoListDto toDoList, Guid userId);
        IEnumerable<ToDoListDto> GetLists();

        ToDoListDto GetSingleList(Guid id);

        //IEnumerable<ToDoListDto> GetCurrentUserLists(System.Security.Principal.IIdentity identity, string userId);

        IEnumerable<ToDoListDto> GetCurrentUserLists(Guid userId);

        //void DeleteList(Guid? id);

        ToDoListDto DeleteList();
        ToDoListDto EditList(ToDoListDto list);

        ToDoListDto EditTitleColor(ToDoListDto list);

        IEnumerable<ToDoListDto> SortLists(UserDto user);

    }
}