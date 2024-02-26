using EShop.Core.OrderAggregate;
using EShop.Core.UserAggregate;

namespace EShop.Core.Interfaces;

public interface IOrder : IIdentifiable<int>
{
    public IUser User { get; }
    public ICollection<OrderItem> Items { get; }
    public DateTime DateTime { get; }
    public PaymentMethod PaymentMethod { get; }
}
