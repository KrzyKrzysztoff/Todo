using Microsoft.EntityFrameworkCore;
using System;
using ToDo.API.Databases;
using ToDo.API.DTOs;
using ToDo.API.Models;

namespace ToDo.API.Repositories
{
    public class TodoRepository(TodoDbContext context) : ITodoRepository
    {
        private readonly TodoDbContext _context = context;

        public async Task<int> CreateAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo.Id; // EF set this automatically
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await GetByIdAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<Todo>> GetIncomingAsync(DateTime from, DateTime to)
        {
            return await _context.Todos
                .Where(t => t.ExpireDate >= from && t.ExpireDate <= to)
                .ToListAsync();
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }
    }
}
