﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Models
{
    public class ToDoListDto
    {
        [Key]
        public Guid ListId { get; set; }
        public string ListDateTime { get; set; }
        public string ListTitle { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public string TitleColor { get; set; }
        public Guid UserId { get; set; }
    }
}
