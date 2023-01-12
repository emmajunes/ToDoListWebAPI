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

        public TaskDto GetIndividualTask(Guid taskId)
        {
            return _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        public void DeleteTask(Guid taskId)
        {
            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            _dbContext.Tasks.Remove(selectedTask);
            _dbContext.SaveChanges();


        }

        public TaskDto EditTask(Guid taskId, string title, string description, string prio)
        {
            //var json = FileManagerToDoList.GetCurrentLoggedInUsersLists();

            //var currentList = json[listId - 1];

            var selectedTask = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            selectedTask.TaskTitle = title;
            selectedTask.TaskDescription = description;
            selectedTask.TaskPrio = prio;

            _dbContext.SaveChanges();

            return selectedTask;
        }
    }
}
