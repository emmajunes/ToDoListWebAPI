using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface ITaskService
    {
        ToDoListDto AddTask(string taskTitle, string taskDescription, string taskPrio);
        IEnumerable<TaskDto> GetTasks(Guid listId);

        TaskDto GetSingleTask(Guid taskId);

        void DeleteTask();

        TaskDto EditTask(string? title, string? description, string? prio);

        TaskDto ToggleTask(bool completed);
    }
}
