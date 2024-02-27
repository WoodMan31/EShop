using Monitor = EShop.Core.ProductAggregate.Monitor;

namespace EShop.Tests.Core.ProductAggregate;

public sealed class MonitorTests
{
    [Fact]
    public void Should_HaveAllPropertiesInitialized_When_Created()
    {
        // Arrange
        Monitor monitor;

        // Act
        monitor = new Monitor
        {
            AvailableQuantity = 1,
            Description = "description",
            Name = "name",
            Price = 10m,
            RefreshRate = 10,
            ResolutionWidth = 1920,
            ResolutionHeight = 1080
        };

        // Assert
        Assert.Equal(1, monitor.AvailableQuantity);
        Assert.Equal("description", monitor.Description);
        Assert.Equal("name", monitor.Name);
        Assert.Equal(10m, monitor.Price);
        Assert.Equal(10, monitor.RefreshRate);
        Assert.Equal(1920, monitor.ResolutionWidth);
        Assert.Equal(1080, monitor.ResolutionHeight);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_PriceIsLessThanOrEqualToZero(int invalidPrice)
    {
        // Arrange
        var monitor = new Monitor
        {
            AvailableQuantity = 1,
            Description = "description",
            Name = "name",
            Price = 10m,
            RefreshRate = 10,
            ResolutionWidth = 1920,
            ResolutionHeight = 1080
        };

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => monitor.Price = invalidPrice);

        // Assert
        Assert.Equal(10m, monitor.Price);
    }

    [Fact]
    public void Should_Throw_When_AvailableQuantityIsLessThanZero()
    {
        // Arrange
        var monitor = new Monitor
        {
            AvailableQuantity = 1,
            Description = "description",
            Name = "name",
            Price = 10m,
            RefreshRate = 10,
            ResolutionWidth = 1920,
            ResolutionHeight = 1080
        };

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => monitor.Price = -1);

        // Assert
        Assert.Equal(1, monitor.AvailableQuantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_RefreshRateIsLessThanOrEqualToZero(int invalidRefreshRate)
    {
        // Arrange
        var monitor = new Monitor
        {
            AvailableQuantity = 1,
            Description = "description",
            Name = "name",
            Price = 10m,
            RefreshRate = 10,
            ResolutionWidth = 1920,
            ResolutionHeight = 1080
        };

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => monitor.RefreshRate = invalidRefreshRate);

        // Assert
        Assert.Equal(10, monitor.RefreshRate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_ResolutionWidthIsLessThanOrEqualToZero(int invalidResolutionWidth)
    {
        // Arrange
        var monitor = new Monitor
        {
            AvailableQuantity = 1,
            Description = "description",
            Name = "name",
            Price = 10m,
            RefreshRate = 10,
            ResolutionWidth = 1920,
            ResolutionHeight = 1080
        };

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => monitor.ResolutionWidth = invalidResolutionWidth);

        // Assert
        Assert.Equal(1920, monitor.ResolutionWidth);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_ResolutionHeightIsLessThanOrEqualToZero(int invalidResolutionHeight)
    {
        // Arrange
        var monitor = new Monitor
        {
            AvailableQuantity = 1,
            Description = "description",
            Name = "name",
            Price = 10m,
            RefreshRate = 10,
            ResolutionWidth = 1920,
            ResolutionHeight = 1080
        };

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => monitor.ResolutionHeight = invalidResolutionHeight);

        // Assert
        Assert.Equal(1080, monitor.ResolutionHeight);
    }
}
