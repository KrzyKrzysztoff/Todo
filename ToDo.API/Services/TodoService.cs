using AutoMapper;
using ToDo.API.DTOs;
using ToDo.API.Exceptions;
using ToDo.API.Models;
using ToDo.API.Repositories;

namespace ToDo.API.Services
{
    public class TodoService(ITodoRepository repository, IMapper mapper) : ITodoService
    {
        private readonly ITodoRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<int> CreateAsync(TodoDto todoDto)
        {
            var todo = _mapper.Map<Todo>(todoDto);
            return await _repository.CreateAsync(todo);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            var todos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TodoDto>>(todos);
        }

        public async Task<TodoDto?> GetByIdAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);

            return _mapper.Map<TodoDto>(todo);
        }

        public async Task<IEnumerable<TodoDto>> GetIncomingAsync(DateTime from, DateTime to)
        {
            var todos = await _repository.GetIncomingAsync(from, to);
            return _mapper.Map<IEnumerable<TodoDto>>(todos);
        }

        public async Task UpdateAsync(int id, TodoDto todoDto)
        {
            var todo = await _repository.GetByIdAsync(id) ?? throw new NotFoundException();

            _mapper.Map(todoDto, todo);
            await _repository.UpdateAsync(todo);
        }
        public async Task SetTodoPercentCompleteAsync(int id, decimal percent)
        {
            var todo = await _repository.GetByIdAsync(id) ?? throw new NotFoundException();

            todo.CompletePercent = percent;

            await _repository.UpdateAsync(todo);
        }
    }
}
