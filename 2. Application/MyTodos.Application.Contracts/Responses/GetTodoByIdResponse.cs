namespace MyTodos.Application.Contracts.Responses;

public record GetTodoByIdResponse(
    int Id,
    string Title,
    string Assignee,
    string? Description
);