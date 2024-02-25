namespace EShop.Core.Interfaces;

public interface IUser
{ 
    public string FullName { get; }
    public string Email { get; }
    public string Password { get; }
    public string Address { get; }
    public HashSet<string> CardNumbers { get; }
    public IShoppingCart ShoppingCart { get; }
}

