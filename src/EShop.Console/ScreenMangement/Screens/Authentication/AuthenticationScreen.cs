using EShop.Console.Attributes;
using Spectre.Console;

namespace EShop.Console.ScreenMangement.Screens.Authentication;

[AllowAnonymous]
public sealed class AuthenticationScreen : IScreen
{
    private readonly IAnsiConsole _console;

    public ScreenData BackScreenData { get; set; } = default!;

    public AuthenticationScreen(IAnsiConsole console)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
    }

    public Task<ScreenEndAction> ShowAsync()
    {
        var screenData = _console.Prompt(
            new SelectionPrompt<ScreenData>()
                .UseConverter(screenData => screenData switch
                {
                    _ when screenData.Type == typeof(AuthorizationScreen) => "Авторизация",
                    _ when screenData.Type == typeof(RegistrationScreen) => "Регистрация",

                    _ => throw new NotImplementedException()
                })
                .Title("[bold]Выберите действие:[/]")
                .AddChoices(
                    new ScreenData<AuthorizationScreen>(screen => screen.BackScreenData = BackScreenData),
                    new ScreenData<RegistrationScreen>(screen => screen.BackScreenData = BackScreenData)));

        return Task.FromResult(ScreenEndAction.OpenScreen(screenData));
    }
}
