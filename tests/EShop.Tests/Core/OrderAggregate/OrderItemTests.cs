using EShop.Core.OrderAggregate;
using EShop.Core.ProductAggregate;

namespace EShop.Tests.Core.OrderAggregate;

public sealed class OrderItemTests
{
    [Fact]
    public void Should_HasAllPropertiesInitialized_When_Created()
    {
        // Arrange
        OrderItem orderItem;

        var product = new ComputerMouse
        {
            AvailableQuantity = 1,
            ConnectionType = "connection type",
            Description = "description",
            Dpi = 50,
            Name = "name",
            Price = 10m,
            SensorType = "sensor type",
            Weight = 200
        };


        // Act
        orderItem = new OrderItem
        {
            Product = product,
            Quantity = 1
        };

        // Assert
        Assert.Equal(product, orderItem.Product);
        Assert.Equal(1, orderItem.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_QuantityIsLessThanOrEqualToZero(int invalidQuantity)
    {
        // Arrange
        var orderItem = new OrderItem
        {
            Product = new ComputerMouse
            {
                AvailableQuantity = 1,
                ConnectionType = "connection type",
                Description = "description",
                Dpi = 50,
                Name = "name",
                Price = 10m,
                SensorType = "sensor type",
                Weight = 200
            },
            Quantity = 1
        };

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => orderItem.Quantity = invalidQuantity);

        // Assert
        Assert.Equal(1, orderItem.Quantity);
    }
}
