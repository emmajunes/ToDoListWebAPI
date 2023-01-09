using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.API.Models
{
    public class ToDoListDto
    {
        public ToDoListDto()
        {
            Tasks = new List<TaskDto>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ListId { get; set; }
        public string ListDateTime { get; set; }
        public string ListTitle { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }
        public string TitleColor { get; set; }
        public Guid UserId { get; set; }
    }
}
