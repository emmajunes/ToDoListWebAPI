﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Models
{
    public class UserDto
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Access { get; set; }

       
    }
}
