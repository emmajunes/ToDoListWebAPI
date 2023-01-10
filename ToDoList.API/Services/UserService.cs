using Microsoft.AspNetCore.Identity;
using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoListContext _dbContext;
        public UserService(ToDoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDto CreateUser(string username, string email, string password)
        {

            var newUser = new UserDto()
            {
                Username = username,
                Email = email,
                Password = password,
                Access = "User",
                Id = Guid.NewGuid(),
            };
            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();

            //Users.Add(newUser);

            return newUser;

            //GetUsers();

            //Console.WriteLine("\nCREATE ACCOUNT");

            //Console.WriteLine("\nEnter username: ");
            //string username = Console.ReadLine();

            //if (String.IsNullOrWhiteSpace(username))
            //{
            //    Console.WriteLine("Username cannot be empty. Try again!");
            //    CreateUser();
            //    return;
            //}

            //foreach (var user in Users)
            //{
            //    if (user.Username == username)
            //    {
            //        Console.WriteLine("Username already exists. Try again!");
            //        CreateUser();
            //        return;
            //    }
            //}

            //Console.WriteLine("Enter email: ");
            //string email = Console.ReadLine();

            //var isValidEmail = ValidateEmail(email);

            //if (!isValidEmail)
            //{
            //    Console.WriteLine("Invalid email!");
            //    CreateUser();
            //    return;
            //}

            //Console.WriteLine("Enter a strong password: ");
            //string password = HidePassword();

            //var isValidPassword = ValidatePassword(password);

            //if (!isValidPassword || password.Contains("\u0000") || password.Length < 8)
            //{
            //    Console.WriteLine("Invalid password! ");
            //    Console.WriteLine("Password needs to be at least 8 characters long.");
            //    Console.WriteLine("Include a specialcharacter.");
            //    Console.WriteLine("Include at least one uppercase character.");
            //    Console.WriteLine("Include at least one lowercase character.");
            //    Console.WriteLine("Include at least one number.\n");
            //    CreateUser();
            //    return;
            //}


            //MenuManager.UserPosition = Users.IndexOf(newUser);
            //FileManager.UpdateJson(_path, Users);

        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = _dbContext.User.SingleOrDefault(x => x.Username == username && password == password);

            return user;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _dbContext.User.ToList();

        }

        public UserDto GetIndividualUser(Guid id)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            return selectedUser;
        }

        public UserDto EditProfile(Guid id, string? username, string? email, string? password)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            selectedUser.Username = username == null ? selectedUser.Username : username;
            selectedUser.Email = email == null ? selectedUser.Email : email;
            selectedUser.Password = password == null ? selectedUser.Password : password;

            _dbContext.SaveChanges();

            return selectedUser;
        }
        public void DeleteUser(Guid id)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);
            _dbContext.User.Remove(selectedUser);
            _dbContext.SaveChanges();

        }

    }
}
