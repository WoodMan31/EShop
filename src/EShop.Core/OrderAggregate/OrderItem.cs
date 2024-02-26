using EShop.Core.ProductAggregate;

namespace EShop.Core.OrderAggregate;

public class OrderItem
{
    private ProductBase _product = default!;
    private int _quantity;

    public required ProductBase Product
    {
        get => _product;
        init => _product = value ?? throw new ArgumentNullException(nameof(value));
    }
    public required int Quantity
    {
        get => _quantity;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Quantity must be greater than 0.");
            }

            _quantity = value;
        }
    }
}
