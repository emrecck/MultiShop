using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories
{
    public interface IUnitOfWork
    {
        IEfCargoCompanyRepository CargoCompanyRepository { get; }
        IEfCargoDetailRepository CargoDetailRepository { get; }
        IEfCargoOperationRepository CargoOperationRepository { get; }
        IEfCargoReceiverCustomerRepository CargoReceiverCustomerRepository { get; }
        IEfCargoSenderCustomerRepository CargoSenderCustomerRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
