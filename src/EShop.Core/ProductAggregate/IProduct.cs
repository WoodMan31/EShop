namespace EShop.Core.ProductAggregate;

public interface IProduct : IIdentifiable<int>
{
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int AvailableQuantity { get; }
}
