using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.API.Models
{

    public class TaskDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Priority TaskPrio { get; set; }
        public string TaskTitle { get; set; }

        public string TaskDescription { get; set; }
        public bool Completed { get; set; }

        [ForeignKey("ToDoListDtoId")]
        public Guid ToDoListDtoId { get; set; }
    }
}
