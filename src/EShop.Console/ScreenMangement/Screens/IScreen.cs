namespace EShop.Console.ScreenMangement.Screens;

public interface IScreen
{
    public Task<ScreenEndAction> ShowAsync();
}
