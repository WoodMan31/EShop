using EShop.Console.Extensions;
using EShop.Core.OrderAggregate;
using EShop.Core.UserAggregate;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace EShop.Console.ScreenMangement.Screens;

public sealed class CartScreen : IScreen
{
    private readonly IAnsiConsole _console;
    private readonly DatabaseContext _context;
    private readonly UserDataProvider _userDataProvider;

    public bool ShowOrderIsSuccessfullyMadeMessage { get; set; } = false;

    public CartScreen(IAnsiConsole console, DatabaseContext context, UserDataProvider userDataProvider)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
    }

    public async Task<ScreenEndAction> ShowAsync()
    {
        var user = await _context.Users
            .Include(user => user.ShoppingCart.Items)
                .ThenInclude(item => item.Product)
            .SingleAsync(user => user.Email == _userDataProvider.Email);

        _console.Write(CreateCartRender(user.ShoppingCart));
        _console.WriteLine();

        if (ShowOrderIsSuccessfullyMadeMessage)
        {
            _console.MarkupLine("[green]Заказ оформлен успешно![/]");
            _console.WriteLine();
        }

        var wantMakeOrder = _console.Prompt(
            new SelectionPrompt<bool>()
                .UseConverter(value => value ? "Оформить заказ" : "[gray]Назад[/]")
                .AddChoices(user.ShoppingCart.Items.Any() ? [false, true] : [false]));

        if (!wantMakeOrder)
        {
            return ScreenEndAction.Back();
        }

        var paymentInfo = _console.Prompt(
            new SelectionPrompt<PaymentMethod>()
                .Title("[bold]Выберите метод оплаты:[/]")
                .UseConverter(info => info.Type switch
                {
                    PaymentType.Cash => "Наличный расчёт",
                    PaymentType.CreditCard => $"Карта {info.CardNumber!.FormatAsCardNumber()}",

                    _ => throw new NotImplementedException()
                })
                .AddChoices([PaymentMethod.Cash()])
                .AddChoices(user.CardNumbers.Select(PaymentMethod.CreditCard)));

        var order = new Order(DateTime.UtcNow, paymentInfo);
        foreach (var item in user.ShoppingCart.Items)
        {
            order.Items.Add(new OrderItem
            {
                Product = item.Product,
                Quantity = item.Quantity
            });

            item.Product.AvailableQuantity -= item.Quantity;
        }

        user.ShoppingCart.Items.Clear();
        user.Orders.Add(order);
        await _context.SaveChangesAsync();

        return ScreenEndAction.ReloadScreen(new ScreenData<CartScreen>(screen => screen.ShowOrderIsSuccessfullyMadeMessage = true));
    }

    private static IRenderable CreateCartRender(IShoppingCart shoppingCart)
    {
        var table = new Table()
            .HorizontalBorder()
            .Collapse()
            .AddColumns("Товар", "Кол-во", string.Empty, "Цена", string.Empty, "Стоимость");

        foreach (var item in shoppingCart.Items)
        {
            table.AddRow(
                item.Product.Name,
                item.Quantity.ToString(),
                "*",
                $"${item.Product.Price}",
                "=",
                $"${item.Quantity * item.Product.Price}");
        }

        table.AddEmptyRow();

        var totalCost = shoppingCart.Items.Sum(item => item.Quantity * item.Product.Price);
        table.AddRow("Итого", string.Empty, string.Empty, string.Empty, string.Empty, $"${totalCost}");

        return table;
    }
}
