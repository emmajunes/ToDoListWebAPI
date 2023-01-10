using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.API.Models
{
    public class ToDoListDto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ListDateTime { get; set; }
        public string ListTitle { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }
        public string TitleColor { get; set; }

        [ForeignKey("UserDtoId")]
        public Guid UserDtoId { get; set; }

        public ToDoListDto()
        {
            Tasks = new List<TaskDto>();
        }
    }
        
}
