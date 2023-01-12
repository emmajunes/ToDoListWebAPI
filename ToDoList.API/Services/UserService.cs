﻿using Microsoft.AspNetCore.Authorization;
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

        public UserDto PromoteUser(Guid id, string access)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            if(access == "Admin")
            {
                selectedUser.Access = "Admin";
            }
            if (access == "Moderator")
            {
                selectedUser.Access = "Moderator";
            }

            _dbContext.SaveChanges();

            return selectedUser;

        }

        public UserDto DemoteUser(Guid id, string access)
        {
            var selectedUser = _dbContext.User.FirstOrDefault(x => x.Id == id);

            if (access == "Moderator")
            {
                selectedUser.Access = "Moderator";
            }
            if (access == "User")
            {
                selectedUser.Access = "User";
            }

            _dbContext.SaveChanges();

            return selectedUser;

        }

    }
}
