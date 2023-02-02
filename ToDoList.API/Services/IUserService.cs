using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IUserService
    {
        UserDto LogOut();
        UserDto Login(UserDto user);
        UserDto CreateUser(UserDto user);
        UserDto GetSingleUser(Guid id);
        void ChangeSortType(UserDto user);
        UserDto EditProfile(Guid id, UserDto user);
        IEnumerable<UserDto> GetUsers();
        UserDto DeleteUser(Guid? id);
        UserDto DeleteUserForAdmin(UserDto user);
        UserDto Authenticate(string username, string password);
        UserDto PromoteUser(UserDto user);
        UserDto DemoteUser(UserDto user);
    }
}
