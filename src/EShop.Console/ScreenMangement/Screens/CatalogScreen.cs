using EShop.Core.ProductAggregate;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace EShop.Console.ScreenMangement.Screens;

public sealed class CatalogScreen : IScreen
{
    private readonly IAnsiConsole _console;
    private readonly DatabaseContext _context;

    public CatalogScreen(IAnsiConsole console, DatabaseContext context)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ScreenEndAction> ShowAsync()
    {
        var products = await _context.Products
            .OrderBy(product => product.AvailableQuantity > 0 ? 0 : 1)
            .ThenBy(product => product.Name)
            .ToListAsync();

        if (products.Count == 0)
        {
            _console.MarkupLine("[bold]Каталог товаров[/]");
            _console.WriteLine();
            _console.MarkupLine("[gray]Каталог товаров пуст.[/]");
            _console.Input.ReadKey(false);

            return ScreenEndAction.Back();
        }

#pragma warning disable CS8714
#pragma warning disable CS8600
#pragma warning disable CS8622
        var product = _console.Prompt(new SelectionPrompt<ProductBase?>()
            .Title("[bold]Каталог товаров[/]")
            .AddChoices((ProductBase)null)
            .AddChoices(products)
            .UseConverter(product => product is null
                ? "[gray]Назад[/]"
                : $"{product.Name} (${product.Price})"));
#pragma warning restore CS8622
#pragma warning restore CS8600
#pragma warning restore CS8714

        if (product is null)
        {
            return ScreenEndAction.Back();
        }

        return ScreenEndAction.OpenScreen(new ScreenData<ProductDetailsScreen>(screen => screen.ProductId = product.Id));
    }
}
