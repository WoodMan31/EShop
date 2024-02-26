using EShop.Console.Attributes;
using EShop.Console.ScreenMangement.Screens;
using EShop.Console.ScreenMangement.Screens.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Reflection;

namespace EShop.Console.ScreenMangement;

public sealed partial class ScreenManager
{
    private readonly IAnsiConsole _console;
    private readonly IServiceProvider _serviceProvider;
    private readonly UserDataProvider _userDataProvider;
    private readonly Stack<ScreenData> _history = new();
    private bool _isStarted = false;

    public ScreenManager(IAnsiConsole console, IServiceProvider serviceProvider, UserDataProvider userDataProvider)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
    }

    public async Task StartAsync(ScreenData screenData)
    {
        ArgumentNullException.ThrowIfNull(screenData);

        if (_isStarted)
        {
            throw new InvalidOperationException("Screen manager has already been started.");
        }

        _isStarted = true;
        _console.Clear();

        while (true)
        {
            if (!_userDataProvider.IsAuthorized && screenData.Type.GetCustomAttribute<AllowAnonymousAttribute>() is null)
            {
                var originalScreenData = screenData.Clone();
                screenData = new ScreenData<AuthenticationScreen>(screen => screen.BackScreenData = originalScreenData);
            }

            await using var scope = _serviceProvider.CreateAsyncScope();

            var screen = (IScreen)scope.ServiceProvider.GetRequiredService(screenData.Type);
            screenData.PrepareAction?.Invoke(screen);

            var screenEndAction = await screen.ShowAsync();

            if (screenEndAction.Type is ScreenEndActionType.None ||
                screenEndAction.Type is ScreenEndActionType.Back && !_history.Any())
            {
                break;
            }

            switch (screenEndAction.Type)
            {
                case ScreenEndActionType.OpenScreen:
                    _history.Push(screenData);
                    screenData = screenEndAction.OpenScreenData!;
                    break;

                case ScreenEndActionType.ReloadScreen:
                    if (screenEndAction.OpenScreenData is null)
                    {
                        break;
                    }
                    
                    if (screenEndAction.OpenScreenData.Type != screenData.Type)
                    {
                        throw new InvalidOperationException();
                    }

                    screenData = screenEndAction.OpenScreenData!;
                    break;

                case ScreenEndActionType.Back:
                    screenData = _history.Pop();
                    break;

                default:
                    throw new NotImplementedException();
            };

            _console.Clear();
        };
    }
}