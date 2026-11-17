using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Persistence;

public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Ordering> Orders => Set<Ordering>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ordering>()
            .HasMany(order => order.OrderDetails)
            .WithOne()
            .HasForeignKey(detail => detail.OrderId);

        modelBuilder.Entity<Ordering>().Property(order => order.TotalPrice).HasPrecision(18, 2);
        modelBuilder.Entity<OrderDetail>().Property(detail => detail.Price).HasPrecision(18, 2);
        modelBuilder.Entity<OrderDetail>().Property(detail => detail.UnitPrice).HasPrecision(18, 2);
    }
}
