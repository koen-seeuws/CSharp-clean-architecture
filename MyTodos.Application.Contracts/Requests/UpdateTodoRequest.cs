namespace MyTodos.Application.Contracts.Requests;

public record UpdateTodoRequest(
    int Id,
    string Title,
    string Assignee,
    string? Description
);