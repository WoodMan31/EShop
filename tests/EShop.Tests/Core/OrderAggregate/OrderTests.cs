using EShop.Core.OrderAggregate;

namespace EShop.Tests.Core.OrderAggregate;

public sealed class OrderTests
{
    [Fact]
    public void Should_HasAllPropertiesInitialized_When_Created()
    {
        // Arrange
        Order order;

        // Act
        order = new Order(new DateTime(2024, 01, 01), PaymentMethod.Cash());

        // Assert
        Assert.Equal(new DateTime(2024, 01, 01), order.DateTime);
        Assert.Equal(PaymentMethod.Cash(), order.PaymentMethod);
        Assert.NotNull(order.Items);
    }
}
