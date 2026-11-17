using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MultiShop.Discount.Coupons;

public class DapperContext(IConfiguration configuration) : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    public IDbConnection CreateConnection() => new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
}
