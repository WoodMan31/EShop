using EShop.Core;
using EShop.Core.ProductAggregate;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Rendering;
using Monitor = EShop.Core.ProductAggregate.Monitor;

namespace EShop.Console.ScreenMangement.Screens;

public sealed class ProductDetailsScreen : IScreen
{
    private readonly IAnsiConsole _console;
    private readonly UserDataProvider _userDataProvider;
    private readonly DatabaseContext _context;

    public int ProductId { get; set; }
    public bool ShowProductWasAddedToCartMessage { get; set; } = false;

    public ProductDetailsScreen(IAnsiConsole console, UserDataProvider userDataProvider, DatabaseContext context)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ScreenEndAction> ShowAsync()
    {
        var product = await _context.Products.SingleAsync(product => product.Id == ProductId);

        _console.MarkupLine(@$"[bold]Товар ""{product.Name.EscapeMarkup()}""[/]");
        _console.Write(CreateProductRender(product));
        _console.WriteLine();

        if (ShowProductWasAddedToCartMessage)
        {
            _console.MarkupLine("[green]Товар успешно добавлен в корзину![/]");
            _console.WriteLine();
        }

        var user = await _context.Users
            .Include(user => user.ShoppingCart.Items)
                .ThenInclude(item => item.Product)
            .SingleAsync(user => user.Email == _userDataProvider.Email);

        var shoppingCart = user.ShoppingCart;
        var shoppingCartItem = shoppingCart.Items.SingleOrDefault(item => item.Product.Id == product.Id);
        var availableQuantity = product.AvailableQuantity - (shoppingCartItem?.Quantity ?? 0);

        var shouldBeAddedToCart = _console.Prompt(
            new SelectionPrompt<bool>()
                .UseConverter(value => value
                    ? (shoppingCartItem?.Quantity ?? 0) > 0
                        ? $"Добавить в корзину (в корзине: {shoppingCartItem!.Quantity})"
                        : "Добавить в корзину"
                    : "[gray]Назад[/]")
                .AddChoices(availableQuantity > 0 ? [false, true] : [false]));

        if (!shouldBeAddedToCart)
        {
            return ScreenEndAction.Back();
        }

        var addToCartCount = _console.Prompt(
            new TextPrompt<int>("Введите количество товара:")
                .DefaultValue(1)
                .Validate(count => count >= 1 && count <= availableQuantity)
                .ValidationErrorMessage($"[red]Количество должно быть положительным числом, не превышающим {availableQuantity}.[/]")
                .PromptStyle(new Style(Color.Yellow)));

        if (shoppingCartItem is not null)
        {
            shoppingCartItem.Quantity += addToCartCount;
        }
        else
        {
            shoppingCartItem = new OrderItem
            {
                Product = product,
                Quantity = addToCartCount
            };

            shoppingCart.Items.Add(shoppingCartItem);
        }

        await _context.SaveChangesAsync();

        return ScreenEndAction.ReloadScreen();
    }

    private static IRenderable CreateProductRender(IProduct product)
    {
        var labelStyle = new Style(decoration: Decoration.Bold);

        var table = new Table()
            .HorizontalBorder()
            .HideHeaders()
            .Collapse()
            .AddColumns(string.Empty, string.Empty)
            .AddRow(new Text("Цена:", labelStyle), new Text($"${product.Price}"))
            .AddRow(new Text("В наличии:", labelStyle), new Text($"{product.AvailableQuantity} шт."))
            .AddEmptyRow();

        table = product switch
        {
            Monitor monitor => table
                .AddRow(new Text("Частота обновления:", labelStyle), new Text($"{monitor.RefreshRate} Гц"))
                .AddRow(new Text("Разрешение:", labelStyle), new Text($"{monitor.ResolutionWidth}x{monitor.ResolutionHeight}")),

            ComputerMouse mouse => table
                .AddRow(new Text("DPI:", labelStyle), new Text($"{mouse.Dpi}"))
                .AddRow(new Text("Тип подключения:", labelStyle), new Text(mouse.ConnectionType))
                .AddRow(new Text("Тип сенсора:", labelStyle), new Text(mouse.SensorType))
                .AddRow(new Text("Вес:", labelStyle), new Text($"{mouse.Weight} г.")),

            _ => throw new NotImplementedException()
        };

        table = table
            .AddEmptyRow()
            .AddRow(new Text("Описание:", labelStyle), new Text(product.Description));

        return table;
    }
}
