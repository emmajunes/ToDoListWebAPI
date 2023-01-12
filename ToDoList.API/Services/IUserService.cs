using ToDoList.API.Models;

namespace ToDoList.API.Services
{
    public interface IUserService
    {
        UserDto CreateUser(string username, string email, string password);

        UserDto GetIndividualUser(Guid id);

        UserDto EditProfile(Guid id, string? username, string? email, string? passwword);

        IEnumerable<UserDto> GetUsers();

        void DeleteUser(Guid? id);
        Task<UserDto> Authenticate(string username, string password);

        UserDto PromoteUser(Guid id, Access access);

        UserDto DemoteUser(Guid id, Access access);
    }
}
