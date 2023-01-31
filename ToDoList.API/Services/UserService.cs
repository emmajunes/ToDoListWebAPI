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

        //public UserDto CreateUser(string username, string email, string password, Access? access)
        //{
        //    if(access == null)
        //    {
        //        access = Access.User;
        //    }

        //    var newUser = new UserDto()
        //    {
        //        Username = username,
        //        Email = email,
        //        Password = password,
        //        Access = (Access)access,
        //    };

        //    _dbContext.User.Add(newUser);
        //    _dbContext.SaveChanges();
        //    return newUser;

        //}

        public UserDto CreateUser(UserDto user)
        {
            //if (access == null)
            //{
            //    access = Access.User;
            //}
         
            if(_dbContext.User.Any(x => x.Username == user.Username))
            {
                throw new Exception();
            }

            var newUser = new UserDto()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Id = Guid.NewGuid(),
                Access = user.Access,
            };

            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();
            return newUser;

        }

        public UserDto Login(UserDto user)
        {
            if(user.Username == null || user.Password == null)
            {
                throw new InvalidOperationException();
            }

            var checkUser = Authenticate(user.Username, user.Password);

            if(checkUser == null)
            {
                throw new UnauthorizedAccessException();
            }

            CurrentRecord.Id["UserId"] = checkUser.Id.ToString();

            return user;
        }
        public UserDto Authenticate(string username, string password)
        {
            UserDto user;
            try
            {
                user = _dbContext.User.Single(x => x.Username == username && x.Password == password);
                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _dbContext.User.ToList();

        }

        public UserDto GetSingleUser(Guid id)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            return selectedUser;
        }

        public void ChangeSortType(UserDto user)
        {
            var currentUser = _dbContext.User.FirstOrDefault(x => x.Id == user.Id);
            currentUser.SortBy = user.SortBy;
            _dbContext.SaveChanges();
        }

        public UserDto EditProfile(Guid id, UserDto user)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            selectedUser.Username = user.Username == null ? selectedUser.Username : user.Username;
            selectedUser.Email = user.Email == null ? selectedUser.Email : user.Email;
            selectedUser.Password = user.Password == null ? selectedUser.Password : user.Password;

            _dbContext.SaveChanges();

            return selectedUser;
        }
        public UserDto DeleteUser(Guid? id)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);
            _dbContext.User.Remove(selectedUser);
            _dbContext.SaveChanges();

            return selectedUser;
        }

        public UserDto PromoteUser(UserDto user)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == user.Id);

            if(selectedUser.Access == Access.Admin)
            {
                throw new Exception();
            }
            selectedUser.Access +=1;
            
            _dbContext.SaveChanges();
            return selectedUser;
        }

        public UserDto DemoteUser(UserDto user)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == user.Id);

            if (selectedUser.Access == Access.User)
            {
                throw new Exception();
            }
            selectedUser.Access -= 1;

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
