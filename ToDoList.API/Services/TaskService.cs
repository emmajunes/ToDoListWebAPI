using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public ToDoListDto AddTask(Guid listId, string taskTitle, string taskDescription, string taskPrio)
        {
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.ListId == listId);

            var newTask = new TaskDto()
            {
                ListId = listId,
                TaskTitle = taskTitle,
                TaskDescription = taskDescription,
                TaskPrio = taskPrio,
                Completed = false
            };

            _dbContext.Tasks.Add(newTask); 
            _dbContext.SaveChanges();

            return _dbContext.ToDoList.Include(x => x.Tasks).FirstOrDefault(x => x.ListId == listId);

            //var loggedInUser = UserManager.LoggedInUser;
            //var json = FileManagerToDoList.GetJson();

            //json = json.Where(x => x.UserId == loggedInUser.UserId).ToList();
            //var currentList = json[listId - 1];

            //Console.WriteLine("What task do you want to add?: ");
            //var task = Console.ReadLine();

            //Console.WriteLine("What description do you want to add to the task?: ");
            //var description = Console.ReadLine();

            //if (String.IsNullOrWhiteSpace(task) || String.IsNullOrWhiteSpace(description))
            //{
            //    Console.WriteLine("Input field cannot be empty");
            //    AddTask(listId);
            //    return;
            //}

            //Console.WriteLine("Select a prio 1-5 (optional): ");
            //var prio = Console.ReadLine();

            //if (String.IsNullOrWhiteSpace(prio))
            //{
            //    prio = "none";
            //}


            //currentList.Tasks.Add(newTask);



            //FileManagerToDoList.UpdateTaskJson(new List<ToDoListDto> { currentList });



        }
    }
}
