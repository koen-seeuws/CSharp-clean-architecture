using MyTodos.Core.Domain;

namespace MyTodos.Application.Contracts.Repositories;

public interface ITodoRepository : IRepository<Todo>
{
    Task<ICollection<Todo>> Search(string? title, string? assignee, CancellationToken cancellationToken = default);
}