using System.Security.AccessControl;

namespace MyTodos.Application.Contracts.Responses;

public record GetTodosResponseRecord(
    int Id,
    string Title,
    string Assignee,
    string? Description
);