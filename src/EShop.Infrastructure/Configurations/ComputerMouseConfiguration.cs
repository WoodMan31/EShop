using EShop.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

internal class ComputerMouseConfiguration : IEntityTypeConfiguration<ComputerMouse>
{
    public void Configure(EntityTypeBuilder<ComputerMouse> builder)
    {
        builder.HasBaseType<ProductBase>();
    }
}
