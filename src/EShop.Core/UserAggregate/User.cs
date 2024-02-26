using EShop.Core.Interfaces;
using EShop.Core.OrderAggregate;
using System.Net.Mail;

namespace EShop.Core.UserAggregate;

public sealed class User : IUser
{
    private readonly List<string> _cardNumbers = new();

    private string _fullName = default!;
    private string _email = default!;
    private string _password = default!;
    private string _address = default!;

    ICollection<IOrder> IUser.Orders => Orders.Cast<IOrder>().ToArray();
    IShoppingCart IUser.ShoppingCart => ShoppingCart;

    public int Id { get; private set; }
    public required string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Full name cannot be null or whitespace.", nameof(value));
            }

            _fullName = value;
        }
    }
    public required string Email
    {
        get => _email;
        set
        {
            if (!MailAddress.TryCreate(value, out var _))
            {
                throw new ArgumentException("Invalid e-mail address", nameof(value));
            }

            _email = value;
        }
    }
    public required string Password
    {
        get => _password;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Password cannot be null or whitespace.", nameof(value));
            }

            _password = value;
        }
    }
    public required string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Address cannot be null or whitespace.", nameof(value));
            }

            _address = value;
        }
    }
    public IReadOnlyCollection<string> CardNumbers { get; }
    public List<Order> Orders { get; } = new();
    public ShoppingCart ShoppingCart { get; } = new();

    public User()
    {
        CardNumbers = _cardNumbers.AsReadOnly();
    }

    public void AddCardNumber(string cardNumber)
    {
        if (cardNumber is null)
        {
            throw new ArgumentNullException(nameof(cardNumber));
        }

        if (cardNumber.Length != 16 || cardNumber.Any(symbol => !char.IsDigit(symbol)))
        {
            throw new ArgumentException("Invalid card number", nameof(cardNumber));
        }

        if (CardNumbers.Contains(cardNumber))
        {
            throw new ArgumentException("Card number already exists", nameof(cardNumber));
        }

        _cardNumbers.Add(cardNumber);
    }
}
