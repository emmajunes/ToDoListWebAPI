using Microsoft.EntityFrameworkCore;
using ToDoList.API.Models;

namespace ToDoList.API
{
    public class ToDoListContext : DbContext
    {
        public DbSet<UserDto> User { get; set; }
        public DbSet<ToDoListDto> ToDoList { get; set; }
        public DbSet<TaskDto> Tasks { get; set; } 
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) { }
    }
}