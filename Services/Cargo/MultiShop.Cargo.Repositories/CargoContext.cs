namespace MultiShop.Cargo.Repositories;

using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.Domain.Entites;

public class CargoContext(DbContextOptions<CargoContext> options) : DbContext(options)
{
    public DbSet<CargoDetail> CargoDetails { get; set; }
    public DbSet<CargoSenderCustomer> CargoSenderCustomers { get; set; }
    public DbSet<CargoReceiverCustomer> CargoReceiverCustomers { get; set; }
    public DbSet<CargoCompany> CargoCompanies { get; set; }
    public DbSet<CargoOperation> CargoOperations { get; set; }

}
