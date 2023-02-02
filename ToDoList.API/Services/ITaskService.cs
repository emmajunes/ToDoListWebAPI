using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface ITaskService
    {
        ToDoListDto AddTask(TaskDto task);
        IEnumerable<TaskDto> GetTasks();
        TaskDto GetSingleTask(Guid taskId);
        TaskDto DeleteTask();
        TaskDto EditTask(TaskDto task);
        TaskDto ToggleTask(TaskDto task);
        void ChangeSortTypeForTask(ToDoListDto list);
        IEnumerable<TaskDto> SortTasks(ToDoListDto list);
    }
}
