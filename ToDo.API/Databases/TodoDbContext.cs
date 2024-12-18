using Microsoft.EntityFrameworkCore;
using System;
using ToDo.API.Models;

namespace ToDo.API.Databases
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos { get; set; }
    }
}
