using Microsoft.AspNetCore.Mvc;
using ToDo.API.DTOs;
using ToDo.API.Exceptions;
using ToDo.API.Models;
using ToDo.API.Services;

namespace ToDo.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(ITodoService service) : ControllerBase
    {
        private readonly ITodoService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _service.GetAllAsync();
            return Ok(todos); // 200
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            try
            {
                var todo = await _service.GetByIdAsync(id);
                return Ok(todo); // 200
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoDto todoDto)
        {
            var id = await _service.CreateAsync(todoDto);
            return Ok(id); // 200
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoDto todoDto)
        {
            try
            {
                await _service.UpdateAsync(id, todoDto);
                return NoContent(); // 204 
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404
            }
        }

        [HttpPut("{id}/setPercent")]
        public async Task<IActionResult> SetPercent(int id, [FromBody] decimal percent)
        {
            if (percent < 0 || percent > 100)
            {
                return BadRequest("Percent must be between 0 and 100.");
            }

            try
            {
                await _service.SetTodoPercentCompleteAsync(id, percent);
                return NoContent(); // 204 
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404
            }
        }

        [HttpPut("{id}/markDone")]
        public async Task<IActionResult> MarkDone(int id)
        {
            try
            {
                decimal percent = 100; // because is done

                await _service.SetTodoPercentCompleteAsync(id, percent);
                return NoContent(); // 204 
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404 
            }
        }
    }
}
