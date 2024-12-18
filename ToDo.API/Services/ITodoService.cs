using ToDo.API.DTOs;
using ToDo.API.Models;

namespace ToDo.API.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetAllAsync();
        Task<TodoDto?> GetByIdAsync(int id);
        Task<IEnumerable<TodoDto>> GetIncomingAsync(DateTime from, DateTime to);
        Task<int> CreateAsync(TodoDto todo);
        Task UpdateAsync(int id, TodoDto todoDto);
        Task SetTodoPercentCompleteAsync(int id, decimal percent);
        Task DeleteAsync(int id);
    }
}
