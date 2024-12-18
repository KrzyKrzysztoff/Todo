using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.API.Controllers;
using ToDo.API.DTOs;
using ToDo.API.Exceptions;
using ToDo.API.Services;

namespace ToDo.Tests
{

    public class TodoEndpointsTests
    {
        private readonly Mock<ITodoService> _serviceMock;
        private readonly TodoController _controller;

        public TodoEndpointsTests()
        {
            _serviceMock = new Mock<ITodoService>();
            _controller = new TodoController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllTodos_ShouldReturnOkResultWithTodos()
        {
            // Arrange
            var todos = new List<TodoDto>
            {
                new() { Title = "Test1", Description = "Description1" },
                new() { Title = "Test2", Description = "Description2" }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(todos);

            // Act
            var result = await _controller.GetAllTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTodos = Assert.IsType<List<TodoDto>>(okResult.Value);
            Assert.Equal(2, returnedTodos.Count);
        }

        [Fact]
        public async Task GetTodoById_ShouldReturnNotFound_WhenTodoDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new NotFoundException());

            // Act
            var result = await _controller.GetTodoById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateTodo_ShouldReturnOkResultWithId()
        {
            // Arrange
            var todoDto = new TodoDto { Title = "Test", Description = "Test description" };
            _serviceMock.Setup(s => s.CreateAsync(todoDto)).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateTodo(todoDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }
    }
}
