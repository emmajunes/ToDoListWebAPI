using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
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

        //skapa errorhantering
        //validering epost, password
        //identity?
        //vad ska admin ha tillgång till osv?

        public UserDto CreateUser(string username, string email, string password, Access? access)
        {
            if(access == null)
            {
                access = Access.User;
            }

            var newUser = new UserDto()
            {
                Username = username,
                Email = email,
                Password = password,
                Access = (Access)access,
            };

            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();
            return newUser;

        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = _dbContext.User.SingleOrDefault(x => x.Username == username && x.Password == password);

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
        public void DeleteUser(Guid? id)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);
            _dbContext.User.Remove(selectedUser);
            _dbContext.SaveChanges();
        }

        public UserDto PromoteUser(Guid id, Access access)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            if(access == Access.Admin)
            {
                selectedUser.Access = Access.Admin;
            }
            if (access == Access.Moderator)
            {
                selectedUser.Access = Access.Moderator;
            }

            _dbContext.SaveChanges();
            return selectedUser;
        }

        public UserDto DemoteUser(Guid id, Access access)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            if (access == Access.Moderator)
            {
                selectedUser.Access = Access.Moderator;
            }
            if (access == Access.User)
            {
                selectedUser.Access = Access.User;
            }

            _dbContext.SaveChanges();
            return selectedUser;
        }

        public bool ValidateEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            //foreach (var user in Users)
            //{
            //    if (user.Email == email)
            //    {
            //        Console.WriteLine("\nEmail already exists!");
            //        return false;
            //    }
            //}

            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        //public bool ValidatePassword(string password)
        //{
        //    int validConditions = 0;
        //    foreach (char c in password)
        //    {
        //        if (c >= 'a' && c <= 'z')
        //        {
        //            validConditions++;
        //            break;
        //        }
        //    }
        //    foreach (char c in password)
        //    {
        //        if (c >= 'A' && c <= 'Z')
        //        {
        //            validConditions++;
        //            break;
        //        }
        //    }
        //    if (validConditions == 0) return false;
        //    foreach (char c in password)
        //    {
        //        if (c >= '0' && c <= '9')
        //        {
        //            validConditions++;
        //            break;
        //        }
        //    }
        //    if (validConditions == 1) return false;
        //    if (validConditions == 2)
        //    {
        //        char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '!', '/', '?', '*', '-', '[', ']', '"', '(', ')', '{', '}', '~', '¤', '´' };
        //        if (password.IndexOfAny(special) == -1) return false;
        //    }
        //    return true;
        //}

    }
}
