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

        public ToDoListDto CreateList(string title, string color)
        {
            //var json = FileManagerToDoList.GetJson();

            //Console.Write("Enter a title of the new list: ");
            ////var title = (Console.ReadLine());

            //if (String.IsNullOrWhiteSpace(item.ListTitle))
            //{
            //    //Console.WriteLine("List title cannot be empty");

            //    throw new ArgumentException("List title cannot be empty");
            //    //CreateList();
            //    return null;
            //}

            var newList = new ToDoListDto()
            {
                ListDateTime = DateTime.Now.ToString(),
                ListId = Guid.NewGuid(),
                ListTitle = title,
                TitleColor = color,
                Tasks = new List<TaskDto>(),
                UserId = Guid.NewGuid() //koppla till inloggad person
            };

            //json.Add(newList);

            //FileManagerToDoList.UpdateJson(json);

            //Console.Clear();
            //Console.WriteLine("New created list: " + item.ListTitle);

            //var userJson = FileManagerToDoList.GetCurrentLoggedInUsersLists();
            //int createdList = userJson.Count;
            //string color = "Red";
            //ColorList(newList, color);
            _dbContext.ToDoList.Add(newList);
            _dbContext.SaveChanges();
            
            //Console.WriteLine(newList.ListTitle);
            return newList;
        }

        public IEnumerable<ToDoListDto> GetLists()
        {

            //var loggedinUser = UserManager.LoggedInUser;
            //var json = FileManagerToDoList.GetJson();

            return _dbContext.ToDoList.ToList();

            //json = json.Where(x => x.UserId == loggedinUser.UserId).ToList();


            //Dictionary<string, int> colors = new()
            //{
            //    { "Magenta", 13 },
            //    { "Yellow", 14 },
            //    { "Blue", 9 },
            //    { "Red", 12 },
            //    { "Cyan", 11 },
            //    { "White", 15 }
            //};

            //Console.WriteLine("\nOVERVIEW OF LISTS: \n");

            //int index = 1;
            //foreach (var list in json)
            //{
            //    Console.ForegroundColor = (ConsoleColor)colors[list.TitleColor];

            //    Console.WriteLine($"[{index}] {list.ListTitle}");
            //    index++;
            //    Console.ForegroundColor = ConsoleColor.White;
            //}
        }

        public ToDoListDto GetIndividualList(Guid id)
        {
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.ListId == id);

            return selectedList;
        }


        private void ColorList(ToDoListDto todoList, string color)
        {
            todoList.TitleColor = color;
        }

        public void DeleteList(Guid id)
        {
            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.ListId == id);
            _dbContext.ToDoList.Remove(selectedList);
            _dbContext.SaveChanges();
            //var json = FileManagerToDoList.GetCurrentLoggedInUsersLists();

            //AvailableLists();
            //ViewAllLists();
            //int deleteList;

            //try
            //{
            //    Console.Write("\nChoose a list to delete: ");
            //    var deleteIndex = Convert.ToInt32(Console.ReadLine());

            //    if (deleteIndex <= 0 || json.Count < deleteIndex)
            //    {
            //        Console.Clear();
            //        Console.WriteLine("Id does not exist. Try again!");
            //        DeleteList();
            //        return;
            //    }

            //    deleteList = deleteIndex - 1;

            //    Console.WriteLine("Do you want to delete this list y/n? ");
            //    var deleteAnswer = Console.ReadLine().ToUpper();

            //    if (String.IsNullOrWhiteSpace(deleteAnswer))
            //    {
            //        Console.WriteLine("Input field cannot be empty");
            //        DeleteList();
            //        return;
            //    }

            //    if (deleteAnswer == "Y")
            //    {
            //        Console.Clear();
            //        Console.WriteLine($"Deleted list: {json[deleteList].ListTitle}");
            //        json.RemoveAt(deleteList);

            //        var allLists = FileManagerToDoList.GetJson();
            //        allLists.RemoveAll(x => x.UserId == UserManager.LoggedInUser.UserId);
            //        var union = allLists.Union(json).ToList();
            //        FileManagerToDoList.UpdateJson(union);
            //        return;
            //    }

            //    else if (deleteAnswer == "N")
            //    {
            //        return;
            //    }

            //    else
            //    {
            //        Console.WriteLine("Answer needs to be a letter of y or n");
            //        DeleteList();
            //        return;
            //    }
            //}

            //catch (ArgumentOutOfRangeException)
            //{
            //    Console.Clear();
            //    Console.WriteLine("Id does not exist. Try again!");
            //    DeleteList();
            //    return;
            //}
            //catch (FormatException)
            //{
            //    Console.Clear();
            //    Console.WriteLine("Id must be a number. Try again!");
            //    DeleteList();
            //    return;
            //}

        }

        public ToDoListDto EditList(Guid id, string title)
        {
            //var json = FileManagerToDoList.GetCurrentLoggedInUsersLists();

            //var currentList = json[listId - 1];

            var selectedList = _dbContext.ToDoList.FirstOrDefault(x => x.ListId == id);

            selectedList.ListTitle = title;

            _dbContext.SaveChanges();

            return selectedList;

            //Console.WriteLine("Write a new title to the list: ");
            //var title = Console.ReadLine();

            //if (String.IsNullOrWhiteSpace(title))
            //{
            //    Console.WriteLine("List title cannot be empty");
            //    //EditList(listId);
            //    //return;
            //}

            //currentList.ListTitle = title;



            //var allLists = FileManagerToDoList.GetJson();
            //allLists.RemoveAll(x => x.UserId == UserManager.LoggedInUser.UserId);
            //var union = allLists.Union(json).ToList();
            //FileManagerToDoList.UpdateJson(union);

            //FileManagerToDoList.UpdateJson(json);
        }

    }
}
