using EShop.Core.ProductAggregate;

namespace EShop.Tests.Core.ProductAggregate;

public sealed class ComputerMouseTests
{
    [Fact]
    public void Should_HaveAllPropertiesInitialized_When_Created()
    {
        // Arrange
        ComputerMouse computerMouse;

        // Act
        computerMouse = new ComputerMouse()
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

        // Assert
        Assert.Equal(1, computerMouse.AvailableQuantity);
        Assert.Equal("connection type", computerMouse.ConnectionType);
        Assert.Equal("description", computerMouse.Description);
        Assert.Equal(50, computerMouse.Dpi);
        Assert.Equal("name", computerMouse.Name);
        Assert.Equal(10m, computerMouse.Price);
        Assert.Equal("sensor type", computerMouse.SensorType);
        Assert.Equal(200, computerMouse.Weight);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_PriceIsLessThanOrEqualToZero(int invalidPrice)
    {
        // Arrange
        var computerMouse = new ComputerMouse()
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
        Assert.Throws<ArgumentOutOfRangeException>(() => computerMouse.Price = invalidPrice);

        // Assert
        Assert.Equal(10m, computerMouse.Price);
    }

    [Fact]
    public void Should_Throw_When_AvailableQuantityIsLessThanZero()
    {
        // Arrange
        var computerMouse = new ComputerMouse()
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
        Assert.Throws<ArgumentOutOfRangeException>(() => computerMouse.Price = -1);

        // Assert
        Assert.Equal(1, computerMouse.AvailableQuantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_DpiIsLessThanOrEqualToZero(int invalidDpi)
    {
        // Arrange
        var computerMouse = new ComputerMouse()
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
        Assert.Throws<ArgumentOutOfRangeException>(() => computerMouse.Dpi = invalidDpi);

        // Assert
        Assert.Equal(50, computerMouse.Dpi);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_WeightIsLessThanOrEqualToZero(int invalidWeight)
    {
        // Arrange
        var computerMouse = new ComputerMouse()
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
        Assert.Throws<ArgumentOutOfRangeException>(() => computerMouse.Weight = invalidWeight);

        // Assert
        Assert.Equal(200, computerMouse.Weight);
    }
}
