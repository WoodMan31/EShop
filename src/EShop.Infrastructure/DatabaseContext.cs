using EShop.Core.UserAggregate;
using EShop.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using EShop.Core.OrderAggregate;

namespace EShop.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ProductBase> Products => Set<ProductBase>();
    public DbSet<Order> Orders => Set<Order>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
