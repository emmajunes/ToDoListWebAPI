using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IUserService
    {
        //UserDto CreateUser(string username, string email, string password, Access? access);

        public UserDto Login(UserDto user);

        UserDto CreateUser(UserDto user);

        UserDto GetSingleUser(Guid id);

        void ChangeSortType(UserDto user);

        //UserDto EditProfile(Guid id, string? username, string? email, string? passwword);

        UserDto EditProfile(Guid id, UserDto user);

        IEnumerable<UserDto> GetUsers();

        UserDto DeleteUser(Guid? id);
        UserDto Authenticate(string username, string password);

        //UserDto PromoteUser(Guid id, Access access);

        UserDto PromoteUser(UserDto user);

        UserDto DemoteUser(UserDto user);
    }
}
