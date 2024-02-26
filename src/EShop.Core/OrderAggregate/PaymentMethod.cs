namespace EShop.Core.OrderAggregate;

public record PaymentMethod
{
    public PaymentType Type { get; private set; }
    public string? CardNumber { get; private set; }

    private PaymentMethod(PaymentType type, string? cardNumber)
    {
        Type = type;
        CardNumber = cardNumber;
    }

    public static PaymentMethod Cash() => new(PaymentType.Cash, null);

    public static PaymentMethod CreditCard(string cardNumber)
    {
        ArgumentNullException.ThrowIfNull(cardNumber);

        return new(PaymentType.CreditCard, cardNumber);
    }
}
