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

        public ToDoListDto AddTask(string taskTitle, string taskDescription, string taskPrio)
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

        public IEnumerable<TaskDto> GetTasks(Guid listId)
        {
             return _dbContext.Tasks.Where(x => x.ToDoListDtoId == listId).ToList();
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

        public TaskDto EditTask(string? title, string? description, string? prio)
        {
            var taskId = Guid.Parse(CurrentRecord.Id["TaskId"]);
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            selectedTask.TaskTitle = title == null ? selectedTask.TaskTitle : title;
            selectedTask.TaskDescription = description == null ? selectedTask.TaskDescription : description;
            selectedTask.TaskPrio = prio == null ? selectedTask.TaskPrio : prio;

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

    }
}
