using EShop.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace EShop.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(user => user.Email);
        
        builder
            .Property(user => user.CardNumbers)
            .HasConversion(
                cardNumbers => JsonSerializer.Serialize(cardNumbers, null as JsonSerializerOptions),
                cardNumbers => JsonSerializer.Deserialize<string[]>(cardNumbers, null as JsonSerializerOptions)!);

        builder.OwnsOne(user => user.ShoppingCart, shoppingCartBuilder =>
        {
            shoppingCartBuilder.OwnsMany(shoppingCart => shoppingCart.Items);
        });
    }
}
