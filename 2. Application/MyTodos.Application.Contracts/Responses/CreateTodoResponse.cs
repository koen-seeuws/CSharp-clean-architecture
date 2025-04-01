namespace MyTodos.Application.Contracts.Responses;

public record CreateTodoResponse(
    int Id,
    string Title,
    string Assignee,
    string? Description
);