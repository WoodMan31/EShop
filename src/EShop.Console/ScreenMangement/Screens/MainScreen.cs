using Spectre.Console;

namespace EShop.Console.ScreenMangement.Screens;

public sealed class MainScreen : IScreen
{
    private readonly IAnsiConsole _console;

    public MainScreen(IAnsiConsole console)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
    }

    public Task<ScreenEndAction> ShowAsync()
    {
#pragma warning disable CS8714
#pragma warning disable CS8622
        var screenType = _console.Prompt(
            new SelectionPrompt<Type?>()
                .Title("[bold]Главное меню[/]")
                .UseConverter(type => type switch
                {
                    _ when type == typeof(CatalogScreen) => "Каталог товаров",
                    _ when type == typeof(CartScreen) => "Корзина",
                    _ when type == typeof(UserDetailsScreen) => "Профиль",
                    null => "[gray]Выход[/]",

                    _ => throw new NotImplementedException()
                })
                .AddChoices(typeof(CatalogScreen), typeof(CartScreen), typeof(UserDetailsScreen), null));
#pragma warning restore CS8622
#pragma warning restore CS8714

        if (screenType is null)
        {
            return Task.FromResult(ScreenEndAction.None());
        }

        return Task.FromResult(ScreenEndAction.OpenScreen(new(screenType)));
    }
}
