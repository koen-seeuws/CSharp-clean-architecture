using AutoMapper;
using MyTodos.Application.Contracts.Commands;
using MyTodos.Application.Contracts.Requests;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Core.Domain;

namespace MyTodos.Application.Business.Mapping;

public class TodoMapping : Profile
{
    public TodoMapping()
    {
        CreateMap<CreateTodoRequest, CreateTodoCommand>();
        CreateMap<UpdateTodoRequest, UpdateTodoCommand>();

        CreateMap<Todo, GetTodoByIdResponse>();
        CreateMap<Todo, GetTodosResponseRecord>();
    }
}