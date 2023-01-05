using Microsoft.AspNetCore.Identity;
using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public class ListService : IListService
    {
        private readonly ToDoListContext _dbContext;
        public ListService(ToDoListContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public ToDoListDto CreateList(ToDoListDto item)
        {
            //var json = FileManagerToDoList.GetJson();

            Console.Write("Enter a title of the new list: ");
            //var title = (Console.ReadLine());

            if (String.IsNullOrWhiteSpace(item.ListTitle))
            {
                //Console.WriteLine("List title cannot be empty");

                throw new ArgumentException("List title cannot be empty");
                //CreateList();
                return null;
            }

            var newList = new ToDoListDto()
            {
                ListDateTime = DateTime.Now.ToString(),
                ListId = Guid.NewGuid(),
                ListTitle = item.ListTitle,
                Tasks = new List<TaskDto>(),
                UserId = Guid.NewGuid()
            };

            //json.Add(newList);

            //FileManagerToDoList.UpdateJson(json);

            //Console.Clear();
            Console.WriteLine("New created list: " + item.ListTitle);

            //var userJson = FileManagerToDoList.GetCurrentLoggedInUsersLists();
            //int createdList = userJson.Count;
            string color = "Red";
            ColorList(newList, color);
            _dbContext.ToDoList.Add(newList);
            _dbContext.SaveChanges();
            //Console.Clear();
            Console.WriteLine(newList.ListTitle);
            return newList;
        }

        private void ColorList(ToDoListDto todoList, string color)
        {
            todoList.TitleColor = color;
        }
    }
}
