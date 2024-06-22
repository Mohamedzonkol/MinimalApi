using Microsoft.EntityFrameworkCore;

namespace MinimalApi
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItem { get; set; }
    }
}
