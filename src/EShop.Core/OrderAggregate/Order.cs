using EShop.Core.Interfaces;
using EShop.Core.UserAggregate;

namespace EShop.Core.OrderAggregate;

public class Order : IOrder
{
    public int Id { get; private set; }
    public User User { get; private set; } = default!;
    IUser IOrder.User => User;
    public List<OrderItem> Items { get; } = new();
    ICollection<OrderItem> IOrder.Items => Items;
    public DateTime DateTime { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; } = default!;

    private Order() { }

    public Order(DateTime dateTime, PaymentMethod paymentInfo)
    {
        DateTime = dateTime;
        PaymentMethod = paymentInfo ?? throw new ArgumentNullException(nameof(paymentInfo));
    }
}
