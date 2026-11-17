using System.Linq.Expressions;

namespace MultiShop.Order.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    T Update(T entity);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
