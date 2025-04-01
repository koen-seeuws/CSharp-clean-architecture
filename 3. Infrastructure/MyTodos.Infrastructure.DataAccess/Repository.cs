using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyTodos.Application.Contracts.Interfaces;

namespace MyTodos.Infrastructure.DataAccess;

public abstract class Repository<TModel>(
    DbContext dbContext
) : IRepository<TModel> where TModel : class
{
    protected DbContext DbContext { get; } = dbContext;
    protected DbSet<TModel> DbSet { get; } = dbContext.Set<TModel>();

    public async Task<IList<TModel>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IList<TModel>> Find(Expression<Func<TModel, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TModel?> FindById(object id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<TModel?> Single(Expression<Func<TModel, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<TModel> Create(TModel toCreate, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(toCreate, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task<IList<TModel>> Create(IList<TModel> toCreate, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(toCreate, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task Update(TModel toUpdate, CancellationToken cancellationToken = default)
    {
        DbSet.Update(toUpdate);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(IList<TModel> toUpdate, CancellationToken cancellationToken = default)
    {
        foreach (var entity in toUpdate)
            DbSet.Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(TModel toDelete, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(toDelete);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(IList<TModel> toDelete, CancellationToken cancellationToken = default)
    {
        DbSet.RemoveRange(toDelete);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        DbSet.RemoveRange(DbSet);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}