using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IListService
    {
        ToDoListDto CreateList(string title, string color, Guid userId);
        IEnumerable<ToDoListDto> GetLists();

        ToDoListDto GetIndividualList(Guid id);

        IEnumerable<ToDoListDto> GetCurrentUserLists(System.Security.Principal.IIdentity identity, string userId);

        void DeleteList(Guid id);

        ToDoListDto EditList(Guid id, string title);

        
    }
}