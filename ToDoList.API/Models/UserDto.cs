using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ToDoList.API.Models
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Access Access { get; set; }
        public SortList SortBy { get; set; } = SortList.Ascendning;
        public ICollection<ToDoListDto> ToDoList { get; set; }

        public UserDto()
        {
            ToDoList = new List<ToDoListDto>();
        }

    }
}
