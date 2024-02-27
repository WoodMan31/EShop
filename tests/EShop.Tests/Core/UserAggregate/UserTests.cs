using EShop.Core.UserAggregate;
namespace EShop.Tests.Core.UserAggregate;

public sealed class UserTests
{
    [Fact]
    public void Should_HaveAllPropertiesInitialized_When_Created()
    {
        // Arrange
        User user;

        // Act
        user = new User
        {
            FullName = "First Last",
            Email = "example@email.com",
            Address = "Some address",
            Password = "password"
        };

        // Assert
        Assert.Equal("First Last", user.FullName);
        Assert.Equal("example@email.com", user.Email);
        Assert.Equal("Some address", user.Address);
        Assert.Equal("password", user.Password);

        Assert.NotNull(user.CardNumbers);
        Assert.NotNull(user.Orders);
        Assert.NotNull(user.ShoppingCart);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234")]
    public void Should_Throw_When_TryingToAddInvalidCardNumber(string invalidCardNumber)
    {
        // Arrange
        var user = new User
        {
            FullName = "First Last",
            Email = "example@email.com",
            Address = "Some address",
            Password = "password"
        };

        // Act
        Assert.Throws<ArgumentException>(() => user.AddCardNumber(invalidCardNumber));

        // Assert
        Assert.Empty(user.CardNumbers);
    }

    [Theory]
    [InlineData("1234123412341234")]
    [InlineData("1111222233334444")]
    public void Should_AddCardNumber_When_TryingToAddValidCardNumber(string validCardNumber)
    {
        // Arrange
        var user = new User
        {
            FullName = "First Last",
            Email = "example@email.com",
            Address = "Some address",
            Password = "password"
        };

        // Act
        user.AddCardNumber(validCardNumber);

        // Assert
        Assert.Equal(validCardNumber, Assert.Single(user.CardNumbers));
    }
}
