namespace EShop.Console.Extensions;

internal static class StringExtensions
{
    public static string FormatAsCardNumber(this string cardNumber)
        => $"{cardNumber![0..4]}-{cardNumber[4..8]}-{cardNumber[8..12]}-{cardNumber[12..]}";
}
