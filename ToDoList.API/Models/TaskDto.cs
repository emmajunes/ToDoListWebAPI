using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.API.Models
{
    
    public class TaskDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string TaskDescription { get; set; }
        public string TaskPrio { get; set; }
        public string TaskTitle { get; set; }
        public bool Completed { get; set; }
        public Guid ListId { get; set; }
    }
}
