using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories
{
    public class UnitOfWork(CargoContext context) : IUnitOfWork
    {
        private IEfCargoCompanyRepository? _cargoCompanyRepository;

        private IEfCargoDetailRepository? _cargoDetailRepository;

        private IEfCargoOperationRepository? _cargoOperationRepository;

        private IEfCargoReceiverCustomerRepository? _cargoReceiverCustomerRepository;

        private IEfCargoSenderCustomerRepository? _cargoSenderCustomerRepository;

        public IEfCargoCompanyRepository CargoCompanyRepository => _cargoCompanyRepository ??= new EFCargoCompanyRepository(context);

        public IEfCargoDetailRepository CargoDetailRepository => _cargoDetailRepository ??= new EFCargoDetailRepository(context);

        public IEfCargoOperationRepository CargoOperationRepository => _cargoOperationRepository ??= new EFCargoOperationRepository(context);

        public IEfCargoReceiverCustomerRepository CargoReceiverCustomerRepository => _cargoReceiverCustomerRepository ??= new EFCargoReceiverCustomerRepository(context);

        public IEfCargoSenderCustomerRepository CargoSenderCustomerRepository => _cargoSenderCustomerRepository ??= new EFCargoSenderCustomerRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditAndSoftDeleteRules();
            return await context.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditAndSoftDeleteRules()
        {
            var now = DateTime.UtcNow;
            //var userId = currentUserService.UserId ?? "System";

            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    //entry.Entity.CreatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    //entry.Entity.UpdatedBy = userId;
                }
            }

            foreach (var entry in context.ChangeTracker.Entries<ISoftDeletable>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = now;
                }
            }
        }

        // Birden fazla SaveChangesAsync veya ExecuteUpdateAsync içeren kritik akışlar için
        public async Task ExecuteInTransactionAsync(
            Func<CancellationToken, Task> operation,
            CancellationToken cancellationToken = default)
        {
            // SQL Server'da EnableRetryOnFailure aktifse ExecutionStrategy kullanımı zorunludur
            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await operation(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

    }
}
