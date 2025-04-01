using Microsoft.EntityFrameworkCore;
using MyTodos.Application.Contracts.Repositories;
using MyTodos.Core.Domain;
using MyTodos.Infrastructure.DataAccess.Configuration;

namespace MyTodos.Infrastructure.DataAccess;

public class TodoRepository(TodoDbContext dbContext) : Repository<Todo>(dbContext), ITodoRepository
{
    public async Task<ICollection<Todo>> Search(string? title, string? assignee, CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrEmpty(title))
            query = query.Where(todo => todo.Title.Contains(title));

        if (!string.IsNullOrEmpty(assignee))
            query = query.Where(todo => todo.Assignee.Contains(assignee));

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
}