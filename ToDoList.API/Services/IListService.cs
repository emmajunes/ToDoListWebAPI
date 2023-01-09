using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IListService
    {
        ToDoListDto CreateList(string title, string color);
        IEnumerable<ToDoListDto> GetLists();

        ToDoListDto GetIndividualList(Guid id);

        void DeleteList(Guid id);

        ToDoListDto EditList(Guid id, string title);

        
    }
}