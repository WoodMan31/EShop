namespace EShop.Console.ScreenMangement;

public class ScreenEndAction
{
    public ScreenEndActionType Type { get; }
    public ScreenData? OpenScreenData { get; }

    private ScreenEndAction(ScreenEndActionType type, ScreenData? nextScreenData = null)
    {
        Type = type;
        OpenScreenData = nextScreenData;
    }

    public static ScreenEndAction None() => new(ScreenEndActionType.None);

    public static ScreenEndAction OpenScreen(ScreenData screenData)
    {
        ArgumentNullException.ThrowIfNull(screenData);

        return new(ScreenEndActionType.OpenScreen, screenData);
    }

    public static ScreenEndAction ReloadScreen(ScreenData? screenData = null)
        => new(ScreenEndActionType.ReloadScreen, screenData);

    public static ScreenEndAction Back() => new(ScreenEndActionType.Back);
}
