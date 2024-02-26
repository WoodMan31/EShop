using EShop.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

internal class MonitorConfiguration : IEntityTypeConfiguration<Core.ProductAggregate.Monitor>
{
    public void Configure(EntityTypeBuilder<Core.ProductAggregate.Monitor> builder)
    {
        builder.HasBaseType<ProductBase>();
    }
}
