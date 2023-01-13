using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ToDoListContext _dbContext;
        public TaskService(ToDoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ToDoListDto AddTask(string taskTitle, string taskDescription, Priority taskPrio)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);

            var newTask = new TaskDto()
            {
                Id = Guid.NewGuid(),
                ToDoListDtoId = listId,
                TaskTitle = taskTitle,
                TaskDescription = taskDescription,
                TaskPrio = taskPrio,
                Completed = false,
            };

            _dbContext.Tasks.Add(newTask);
            _dbContext.SaveChanges();

            return _dbContext.ToDoList.Include(x => x.Tasks).FirstOrDefault(x => x.Id == listId);

        }

        public IEnumerable<TaskDto> GetTasks()
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var currentList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);
            var sortedTasks = SortTasks(currentList.Sortby);

            return sortedTasks;
        }

        public TaskDto GetSingleTask(Guid taskId)
        {
            CurrentRecord.Id["TaskId"] = taskId.ToString();
            return _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        public void DeleteTask()
        {
            var taskId = Guid.Parse(CurrentRecord.Id["TaskId"]);
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            _dbContext.Tasks.Remove(selectedTask);
            _dbContext.SaveChanges();
        }

        public TaskDto EditTask(string? title, string? description, Priority? prio)
        {
            var taskId = Guid.Parse(CurrentRecord.Id["TaskId"]);
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            selectedTask.TaskTitle = title ?? selectedTask.TaskTitle;
            selectedTask.TaskDescription = description ?? selectedTask.TaskDescription;
            selectedTask.TaskPrio = prio ?? selectedTask.TaskPrio;

            _dbContext.SaveChanges();
            return selectedTask;
        }

        public TaskDto ToggleTask(bool completed)
        {
            var taskId = Guid.Parse(CurrentRecord.Id["TaskId"]);
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            selectedTask.Completed =! selectedTask.Completed;

            _dbContext.SaveChanges();
            return selectedTask;
        }

        public void ChangeSortTypeForTask(SortTask sortAlternative)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);

            var currentList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);
            currentList.Sortby = sortAlternative;
            _dbContext.SaveChanges();
        }

        public IEnumerable<TaskDto> SortTasks(SortTask sortAlternative)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);

            var currentTasks = _dbContext.Tasks.Where(x => x.ToDoListDtoId == listId).ToList();

            switch (sortAlternative)
            {
                case SortTask.Priority:
                    currentTasks = currentTasks.OrderByDescending(x => x.TaskPrio).ToList();
                    break;
                case SortTask.Completed:
                    currentTasks = currentTasks.OrderByDescending(x => x.Completed).ToList();
                    break;
            }

            return currentTasks;
        }

    }
}
