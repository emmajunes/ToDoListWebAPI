using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface ITaskService
    {
        ToDoListDto AddTask(string taskTitle, string taskDescription, string taskPrio);
        IEnumerable<TaskDto> GetTasks(Guid listId);

        TaskDto GetIndividualTask(Guid taskId);

        void DeleteTask(Guid taskId);

        TaskDto EditTask(Guid taskId, string title, string description, string prio);
    }
}
