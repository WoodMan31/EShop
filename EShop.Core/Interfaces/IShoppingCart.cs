namespace EShop.Core.Interfaces;

public interface IShoppingCart
{
    public List<IOrderItem> Items { get; }
}

