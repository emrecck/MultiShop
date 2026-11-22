using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using System.Linq.Expressions;

namespace MultiShop.Cargo.Repositories;

public class GenericRepository<Tentity, Ttype>(CargoContext context) : IGenericRepository<Tentity, Ttype> where Tentity : class, IEntity<Ttype>
{
    private readonly DbSet<Tentity> _dbSet = context.Set<Tentity>();

    public async Task<Ttype> AddAsync(Tentity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        return result.Entity.Id;
    }

    public async Task DeleteAsync(Ttype id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public IQueryable<Tentity> GetBy(Expression<Func<Tentity, bool>> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate);
    }

    public IQueryable<Tentity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public async Task<Tentity?> GetByIdAsync(Ttype id)
    {
        return await _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(id));
    }

    public Tentity Update(Tentity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }
}
