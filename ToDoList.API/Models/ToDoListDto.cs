using Microsoft.Extensions.Diagnostics.HealthChecks;
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
        public Color TitleColor { get; set; }
        public SortTask Sortby { get; set; } = SortTask.Priority;

        [ForeignKey("UserDtoId")]
        public Guid UserDtoId { get; set; }

        public ToDoListDto()
        {
            Tasks = new List<TaskDto>();
        }
    }
        
}
