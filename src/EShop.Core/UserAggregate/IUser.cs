using EShop.Core.Interfaces;

namespace EShop.Core.UserAggregate;

public interface IUser : IIdentifiable<int>
{
    public string FullName { get; }
    public string Email { get; }
    public string Password { get; }
    public string Address { get; }
    public IReadOnlyCollection<string> CardNumbers { get; }
    public ICollection<IOrder> Orders { get; }
    public IShoppingCart ShoppingCart { get; }

    public void AddCardNumber(string cardNumber);
}
