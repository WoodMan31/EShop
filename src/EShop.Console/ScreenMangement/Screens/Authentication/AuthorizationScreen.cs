using EShop.Console.Attributes;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace EShop.Console.ScreenMangement.Screens.Authentication;

[AllowAnonymous]
public sealed class AuthorizationScreen : IScreen
{
    private readonly IAnsiConsole _console;
    private readonly DatabaseContext _context;
    private readonly UserDataProvider _userDataProvider;

    public ScreenData BackScreenData { get; set; } = default!;

    public AuthorizationScreen(IAnsiConsole console, DatabaseContext context, UserDataProvider userDataProvider)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
    }

    public async Task<ScreenEndAction> ShowAsync()
    {
        _console.MarkupLine("[bold]Авторизация[/]");
        _console.WriteLine();

        var email = _console.Prompt(
            new TextPrompt<string>("Введите e-mail: ")
                .PromptStyle(new Style(Color.Yellow)));

        var password = _console.Prompt(
            new TextPrompt<string>("Введите пароль: ")
                .PromptStyle(new Style(Color.Yellow))
                .Secret());

        var user = await _context.Users.SingleOrDefaultAsync(user => user.Email == email && user.Password == password);

        if (user is null)
        {
            _console.WriteLine();
            _console.MarkupLine("[red]Некорректное имя или пароль.[/]");
            await _console.Input.ReadKeyAsync(false, CancellationToken.None);

            return ScreenEndAction.Back();
        }

        _userDataProvider.Email = user.Email;

        return ScreenEndAction.OpenScreen(BackScreenData);
    }
}
