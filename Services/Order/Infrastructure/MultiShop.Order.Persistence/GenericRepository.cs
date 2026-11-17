using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Exceptions;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Persistence;

public class GenericRepository<T>(OrderContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _entities = context.Set<T>();

    public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _entities.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _entities.Where(predicate).ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _entities.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _entities.ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _entities.AddAsync(entity, cancellationToken);

    public T Update(T entity)
    {
        var incoming = context.Entry(entity);
        var key = incoming.Metadata.FindPrimaryKey()
            ?? throw new InvalidOperationException($"{typeof(T).Name} has no primary key.");
        var tracked = context.ChangeTracker.Entries<T>()
            .FirstOrDefault(entry => key.Properties.All(property =>
                Equals(entry.Property(property.Name).CurrentValue, incoming.Property(property.Name).CurrentValue)));

        if (tracked is null)
        {
            if (!incoming.IsKeySet)
                throw new InvalidOperationException($"Cannot update {typeof(T).Name} without a primary key.");
            return _entities.Update(entity).Entity;
        }

        if (!ReferenceEquals(tracked.Entity, entity))
            tracked.CurrentValues.SetValues(entity);

        return tracked.Entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException($"{typeof(T).Name} with ID {id} not found.");
        _entities.Remove(entity);
    }
}
