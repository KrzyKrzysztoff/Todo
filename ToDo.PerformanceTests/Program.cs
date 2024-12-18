// See https://aka.ms/new-console-template for more information
using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Moq;
using ToDo.API.Repositories;
using ToDo.API.Services;

BenchmarkRunner.Run<TodoServiceBenchmark>();

public class TodoServiceBenchmark
{
    private readonly TodoService _todoService;
    private readonly Mock<ITodoRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    public TodoServiceBenchmark()
    {
        _repositoryMock = new Mock<ITodoRepository>();
        _mapperMock = new Mock<IMapper>();
        _todoService = new TodoService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Benchmark]

    public async Task GetAllAsyncBenchmark()
    {
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(
        [
            new() { Id = 1, Title = "Test 1", Description = "Description 1" },
            new() { Id = 2, Title = "Test 2", Description = "Description 2" }
        ]);

        await _todoService.GetAllAsync();
    }
}