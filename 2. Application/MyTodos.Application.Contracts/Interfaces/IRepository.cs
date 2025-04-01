using System.Linq.Expressions;

namespace MyTodos.Application.Contracts.Interfaces;

public interface IRepository<TModel> where TModel : class
{
    Task<IList<TModel>> GetAll(CancellationToken cancellationToken = default);
    Task<IList<TModel>> Find(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TModel?> FindById(object id, CancellationToken cancellationToken = default);
    Task<TModel?> Single(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TModel> Create(TModel toCreate, CancellationToken cancellationToken = default);
    Task<IList<TModel>> Create(IList<TModel> toCreate, CancellationToken cancellationToken = default);

    Task Update(TModel toUpdate, CancellationToken cancellationToken = default);
    Task Update(IList<TModel> toUpdate, CancellationToken cancellationToken = default);

    Task Delete(TModel toDelete, CancellationToken cancellationToken = default);
    Task Delete(IList<TModel> toDelete, CancellationToken cancellationToken = default);
    Task DeleteAll(CancellationToken cancellationToken = default);
}