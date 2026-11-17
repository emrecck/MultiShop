using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Persistence;

public class UnitOfWork(OrderContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
