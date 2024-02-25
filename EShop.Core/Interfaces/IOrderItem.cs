namespace EShop.Core.Interfaces;

public interface IOrderItem
{
    public IProduct Product { get; }
    public int Quantity { get; }
}