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
 
        public UserDto CreateUser(UserDto user)
        {       
            if(_dbContext.User.Any(x => x.Username == user.Username)) //göra en för mail?
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
        public UserDto LogOut()
        {
            var loggedOutUser = new UserDto();
            CurrentRecord.Id["UserId"] = "";
            CurrentRecord.Id["UserId"] = loggedOutUser.Id.ToString();

            return loggedOutUser;
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
        public UserDto DeleteUserForAdmin(UserDto user)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == user.Id);
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
    }
}
