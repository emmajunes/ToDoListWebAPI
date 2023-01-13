using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IListService
    {
        ToDoListDto CreateList(string title, Color color, System.Security.Principal.IIdentity identity, string userId);
        IEnumerable<ToDoListDto> GetLists();

        ToDoListDto GetIndividualList(Guid id);

        IEnumerable<ToDoListDto> GetCurrentUserLists(System.Security.Principal.IIdentity identity, string userId);

        void DeleteList(Guid? id);

        ToDoListDto EditList(string title);

        ToDoListDto EditTitleColor(Color color);

        IEnumerable<ToDoListDto> SortLists(SortList sortAlternative, string userId);

        void ChangeSortType(SortList sortAlternative, string userId);

    }
}