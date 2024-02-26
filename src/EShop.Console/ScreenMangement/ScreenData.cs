using EShop.Console.ScreenMangement.Screens;

namespace EShop.Console.ScreenMangement;

public class ScreenData : ICloneable
{
    public Type Type { get; }
    public Action<IScreen>? PrepareAction { get; }

    public ScreenData(Type type, Action<IScreen>? prepareAction = null)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!type.IsAssignableTo(typeof(IScreen)))
        {
            throw new ArgumentException($"Screen type must implement {nameof(IScreen)} interface.", nameof(type));
        }

        Type = type;
        PrepareAction = prepareAction;
    }

    public ScreenData Clone() => new(Type, PrepareAction);

    object ICloneable.Clone() => Clone();
}

public class ScreenData<TScreen> : ScreenData
    where TScreen : IScreen
{
    public ScreenData(Action<TScreen>? prepareScreenAction = null)
        : base(typeof(TScreen), prepareScreenAction is null ? null : screen => prepareScreenAction((TScreen)screen))
    {
    }
}
