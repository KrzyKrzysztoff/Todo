using ToDo.API.DTOs;
using ToDo.API.Models;

namespace ToDo.API.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<IEnumerable<Todo>> GetIncomingAsync(DateTime from, DateTime to);
        Task<int> CreateAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(int id);
    }
}
