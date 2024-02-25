namespace EShop.Core;

public record PaymentMethod(PaymentType Type, string? CardNumber);
