namespace EShop.Console;

public sealed class UserDataProvider
{
    private readonly Action<UserDataProvider>? _onUserChanged;
    private string? _email;

    public UserDataProvider(Action<UserDataProvider>? onUserChanged = null)
    {
        _onUserChanged = onUserChanged ?? throw new ArgumentNullException(nameof(onUserChanged));
    }

    public string? Email
    {
        get => _email;
        set
        {
            var oldValue = _email;
            _email = value;

            if (oldValue != value)
            {
                _onUserChanged?.Invoke(this);
            }
        }
    }
    public bool IsAuthorized => Email is not null;
}
