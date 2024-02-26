namespace EShop.Core.ProductAggregate;

public abstract class ProductBase : IProduct
{
    private string _name = default!;
    private string _description = default!;
    private decimal _price;
    private int _availableQuantity;

    public int Id { get; private set; }
    public required string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }
    public required string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }
    public required decimal Price
    {
        get => _price;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Price must be greater than 0.");
            }

            _price = value;
        }
    }
    public required int AvailableQuantity
    {
        get => _availableQuantity;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Available quantity must be greater than or equal to 0.");
            }

            _availableQuantity = value;
        }
    }
}
