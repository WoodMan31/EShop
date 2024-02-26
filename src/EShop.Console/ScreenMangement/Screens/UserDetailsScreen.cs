using EShop.Console.Extensions;
using EShop.Core.Interfaces;
using EShop.Core.OrderAggregate;
using EShop.Core.UserAggregate;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace EShop.Console.ScreenMangement.Screens;

public sealed class UserDetailsScreen : IScreen
{
    private readonly IAnsiConsole _console;
    private readonly DatabaseContext _context;
    private readonly UserDataProvider _userDataProvider;

    public UserDetailsScreen(IAnsiConsole console, DatabaseContext context, UserDataProvider userDataProvider)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
    }

    public async Task<ScreenEndAction> ShowAsync()
    {
        var user = await _context.Users
            .Include(user => user.Orders.OrderByDescending(order => order.DateTime))
                .ThenInclude(order => order.Items)
                    .ThenInclude(item => item.Product)
            .SingleAsync(user => user.Email == _userDataProvider.Email);

        _console.MarkupLine("[bold]Мой профиль[/]");
        _console.Write(CreateUserInfoRender(user));
        _console.WriteLine();

        if (user.Orders.Any())
        {
            _console.MarkupLine("[bold]История заказов[/]");
            _console.Write(CreateOrdersHistoryRender(user.Orders.Cast<IOrder>().ToArray()));
        }

        await _console.Input.ReadKeyAsync(intercept: true, CancellationToken.None);

        return ScreenEndAction.Back();
    }

    private static IRenderable CreateUserInfoRender(IUser user)
    {
        var labelStyle = new Style(decoration: Decoration.Bold);
        var cardNumbers = string.Join(", ", user.CardNumbers.Select(number => number.FormatAsCardNumber()));

        var table = new Table()
            .HorizontalBorder()
            .HideHeaders()
            .Collapse()
            .AddColumns(string.Empty, string.Empty)
            .AddRow(new Text("ФИО:", labelStyle), new Text(user.FullName))
            .AddRow(new Text("E-mail:", labelStyle), new Text(user.Email))
            .AddRow(new Text("Адрес доставки:", labelStyle), new Text(user.Address))
            .AddRow(new Text("Банковские карты:", labelStyle), new Text(cardNumbers));

        return table;
    }

    private static IRenderable CreateOrdersHistoryRender(ICollection<IOrder> orders)
    {
        var table = new Table()
            .HorizontalBorder()
            .Collapse()
            .AddColumns("Дата", "Метод оплаты", "Товары");

        table.ShowRowSeparators = true;

        foreach (var order in orders)
        {
            var paymentMethod = order.PaymentMethod.Type switch
            {
                PaymentType.Cash => "Наличный расчёт",
                PaymentType.CreditCard => $"Карта {order.PaymentMethod.CardNumber!.FormatAsCardNumber()}",

                _ => throw new NotImplementedException()
            };

            table.AddRow(
                order.DateTime.ToString("g"),
                paymentMethod,
                string.Join(Environment.NewLine, order.Items.Select(item => $"{item.Product.Name} [gray]({item.Quantity} шт.)[/]")));
        }

        return table;
    }
}
