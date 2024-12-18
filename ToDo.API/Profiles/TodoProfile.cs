using AutoMapper;
using ToDo.API.DTOs;
using ToDo.API.Models;

namespace ToDo.API.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoDto, Todo>();
            CreateMap<Todo, TodoDto>();
        }
    }
}
