using AutoMapper;
using Moq;
using ToDo.API.DTOs;
using ToDo.API.Exceptions;
using ToDo.API.Models;
using ToDo.API.Repositories;
using ToDo.API.Services;

namespace ToDo.Tests
{
    public class TodoServiceTests
    {

        private readonly Mock<ITodoRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _mapperMock = new Mock<IMapper>();
            _todoService = new TodoService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldInvokeRepositoryAndReturnId()
        {
            // Arrange
            var todoDto = new TodoDto { Title = "Title", Description = "Description" };
            var todo = new Todo { Id = 1, Title = "Title", Description = "Description" };

            _mapperMock.Setup(m => m.Map<Todo>(todoDto)).Returns(todo);
            _repositoryMock.Setup(r => r.CreateAsync(todo)).ReturnsAsync(todo.Id);

            // Act
            var result = await _todoService.CreateAsync(todoDto);

            // Assert
            Assert.Equal(todo.Id, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException_WhenTodoNotFound()
        {
            // Arrange
            var todoDto = new TodoDto { Title = "Test", Description = "Test description" };
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Todo?)null);

            // Act Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _todoService.UpdateAsync(1, todoDto));
        }
    }
}
