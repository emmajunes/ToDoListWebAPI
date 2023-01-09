using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface ITaskService
    {
        ToDoListDto AddTask(Guid listId, string taskTitle, string taskDescription, string taskPrio);
    }
}
