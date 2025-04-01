namespace MyTodos.Application.Contracts.Requests;

public record CreateTodoRequest(
    string Title,
    string Assignee,
    string? Description
);