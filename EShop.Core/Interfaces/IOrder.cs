namespace EShop.Core.Interfaces;

public interface IOrder
{
    public List<IOrderItem> Items { get; }
    public DateTime DateTime { get; }

}
