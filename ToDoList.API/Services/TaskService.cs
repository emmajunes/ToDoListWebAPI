using Microsoft.AspNetCore.Identity;
using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public class TaskService
    {

        //public void AddTask(TaskDto taskItem)
        //{
        //    var loggedInUser = UserManager.LoggedInUser;
        //    var json = FileManagerToDoList.GetJson();

        //    json = json.Where(x => x.UserId == loggedInUser.UserId).ToList();
        //    var currentList = json[listId - 1];

        //    Console.WriteLine("What task do you want to add?: ");
        //    var task = Console.ReadLine();

        //    Console.WriteLine("What description do you want to add to the task?: ");
        //    var description = Console.ReadLine();

        //    if (String.IsNullOrWhiteSpace(task) || String.IsNullOrWhiteSpace(description))
        //    {
        //        Console.WriteLine("Input field cannot be empty");
        //        AddTask(listId);
        //        return;
        //    }

        //    Console.WriteLine("Select a prio 1-5 (optional): ");
        //    var prio = Console.ReadLine();

        //    if (String.IsNullOrWhiteSpace(prio))
        //    {
        //        prio = "none";
        //    }

        //    var newTask = new TaskDto()
        //    {
        //        TaskTitle = taskItem.TaskTitle,
        //        TaskDescription = taskItem.TaskDescription,
        //        TaskPrio = taskItem.TaskPrio
        //    };

        //    currentList.Tasks.Add(newTask);

        //    FileManagerToDoList.UpdateTaskJson(new List<ToDoListDto> { currentList });

        //}
    }
}
