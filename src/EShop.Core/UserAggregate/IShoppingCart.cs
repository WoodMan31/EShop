using EShop.Core.OrderAggregate;

namespace EShop.Core.UserAggregate;

public interface IShoppingCart
{
    public List<OrderItem> Items { get; }
}
