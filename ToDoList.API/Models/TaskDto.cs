using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Models
{
    
    public class TaskDto
    {
        [Key]
        public Guid ListId { get; set; }
        public string TaskDescription { get; set; }
        public string TaskPrio { get; set; }
        public string TaskTitle { get; set; }

        public bool Completed { get; set; }
    }
}
