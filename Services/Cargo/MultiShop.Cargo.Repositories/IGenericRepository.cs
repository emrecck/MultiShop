using MultiShop.Cargo.Domain.Entites;
using System.Linq.Expressions;
namespace MultiShop.Cargo.Repositories;

public interface IGenericRepository<Tentity, Ttype> where Tentity : class, IEntity<Ttype>
{
    IQueryable<Tentity> GetBy(Expression<Func<Tentity, bool>> predicate);
    IQueryable<Tentity> GetAll();
    Task<Tentity?> GetByIdAsync(Ttype id);
    Task<Ttype> AddAsync(Tentity entity);
    Tentity Update(Tentity entity);
    Task DeleteAsync(Ttype id);
}
