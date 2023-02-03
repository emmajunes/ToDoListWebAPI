using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public ToDoListDto AddTask(TaskDto task)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);

            var newTask = new TaskDto()
            {
                Id = Guid.NewGuid(),
                ToDoListDtoId = listId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskPrio = task.TaskPrio,
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
            var sortedTasks = SortTasks(currentList);

            return sortedTasks;
        }

        public TaskDto GetSingleTask(Guid taskId)
        {
            CurrentRecord.Id["TaskId"] = taskId.ToString();
            var singleTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            return singleTask;
        }

        public TaskDto DeleteTask()
        {
            var taskId = Guid.Parse(CurrentRecord.Id["TaskId"]);
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            _dbContext.Tasks.Remove(selectedTask);
            _dbContext.SaveChanges();

            return selectedTask;
        }

        public TaskDto EditTask(TaskDto task)
        {
            var taskId = Guid.Parse(CurrentRecord.Id["TaskId"]);
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            selectedTask.TaskTitle = task.TaskTitle == null ? selectedTask.TaskTitle : task.TaskTitle;
            selectedTask.TaskDescription = task.TaskDescription == null ? selectedTask.TaskDescription : task.TaskDescription;
            selectedTask.TaskPrio = task.TaskPrio;
            _dbContext.SaveChanges();

            return selectedTask;
        }

        public TaskDto ToggleTask(TaskDto task)
        {
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == task.Id);
            selectedTask.Completed =! selectedTask.Completed;
            _dbContext.SaveChanges();

            return selectedTask;
        }

        public void ChangeSortTypeForTask(ToDoListDto list)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var currentList = _dbContext.ToDoList.FirstOrDefault(x => x.Id == listId);
            currentList.Sortby = list.Sortby;
            _dbContext.SaveChanges();
        }

        public IEnumerable<TaskDto> SortTasks(ToDoListDto list)
        {
            var listId = Guid.Parse(CurrentRecord.Id["ListId"]);
            var currentTasks = _dbContext.Tasks.Where(x => x.ToDoListDtoId == listId).ToList();

            switch (list.Sortby)
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
