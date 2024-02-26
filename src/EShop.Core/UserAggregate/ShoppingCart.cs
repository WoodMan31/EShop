using EShop.Core.OrderAggregate;

namespace EShop.Core.UserAggregate;

public sealed class ShoppingCart : IShoppingCart
{
    public List<OrderItem> Items { get; } = new();
}

