using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface ITaskService
    {
        ToDoListDto AddTask(string taskTitle, string taskDescription, Priority taskPrio);
        IEnumerable<TaskDto> GetTasks(Guid listId);

        TaskDto GetSingleTask(Guid taskId);

        void DeleteTask();

        TaskDto EditTask(string? title, string? description, Priority? prio);

        TaskDto ToggleTask(bool completed);

        void ChangeSortTypeForTask(SortTask sortAlternative);

        IEnumerable<TaskDto> SortTasks(SortTask sortAlternative);
    }
}
