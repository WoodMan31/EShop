namespace EShop.Core.Interfaces;

public interface IProduct
{
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int AvailableQuantity { get; }
}

