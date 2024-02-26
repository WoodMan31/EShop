using EShop.Console.Attributes;
using EShop.Core.UserAggregate;
using EShop.Infrastructure;
using Spectre.Console;
using System.Net.Mail;

namespace EShop.Console.ScreenMangement.Screens.Authentication;

[AllowAnonymous]
public sealed class RegistrationScreen : IScreen
{
    private readonly IAnsiConsole _console;
    private readonly DatabaseContext _context;
    private readonly UserDataProvider _userDataProvider;

    public ScreenData BackScreenData { get; set; } = default!;

    public RegistrationScreen(IAnsiConsole console, DatabaseContext context, UserDataProvider userDataProvider)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
    }

    public async Task<ScreenEndAction> ShowAsync()
    {
        _console.MarkupLine("[bold]Регистрация[/]");
        _console.WriteLine();

        var email = _console.Prompt(
            new TextPrompt<string>("Введите e-mail: ")
                .ValidationErrorMessage("[red]Введённая строка не является валидным e-mail.[/]")
                .Validate(input => MailAddress.TryCreate(input, out var _))
                .PromptStyle(new Style(Color.Yellow)));

        var password = _console.Prompt(
            new TextPrompt<string>("Введите пароль: ")
                .PromptStyle(new Style(Color.Yellow))
                .Secret());

        var fullName = _console.Prompt(
            new TextPrompt<string>("Введите ФИО: ")
                .PromptStyle(new Style(Color.Yellow)));

        var address = _console.Prompt(
            new TextPrompt<string>("Введите адрес доставки: ")
                .PromptStyle(new Style(Color.Yellow)));

        var user = new User
        {
            FullName = fullName,
            Password = password,
            Address = address,
            Email = email
        };

        do
        {
            var wantAddCardNumber = _console.Prompt(
                new SelectionPrompt<bool>()
                    .Title(string.Empty)
                    .UseConverter(value => value ? "Добавить банковскую карту" : "Завершить регистрацию")
                    .AddChoices(true, false));

            if (!wantAddCardNumber)
            {
                break;
            }

            var cardNumber = _console.Prompt(
                new TextPrompt<string>("Введите номер карты: ")
                    .ValidationErrorMessage("[red]Введённая строка не является валидным номером карты или такая карта уже добавлена.[/]")
                    .WithConverter(input => input.Trim().Replace(" ", string.Empty))
                    .Validate(input =>
                    {
                        var cardNumber = input.Trim().Replace(" ", string.Empty);

                        return cardNumber.Length == 16 &&
                            cardNumber.All(char.IsDigit) &&
                            !user.CardNumbers.Contains(cardNumber);
                    })
                    .PromptStyle(new Style(Color.Yellow)));

            user.AddCardNumber(cardNumber);
        }
        while (true);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _userDataProvider.Email = user.Email;

        return ScreenEndAction.OpenScreen(BackScreenData);
    }
}
