using EShop.Console;
using EShop.Console.ScreenMangement;
using EShop.Console.ScreenMangement.Screens;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Reflection;

Console.Title = Constants.Title;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddSingleton(new UserDataProvider(onUserChanged: userDataProvider =>
    Console.Title = userDataProvider.Email is null
        ? Constants.Title
        : $"{Constants.Title} - {userDataProvider.Email}"));

services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
services.AddScoped<DatabaseSeeder>();

services.AddSingleton(AnsiConsole.Console);
services.AddSingleton<ScreenManager>();

var screenTypes = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IScreen)));

foreach (var type in screenTypes)
{
    services.AddScoped(type);
}

var serviceProvider = services.BuildServiceProvider();

await using (var scope = serviceProvider.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await context.Database.MigrateAsync();

	if (!await context.Products.AnyAsync())
	{
		var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
		await seeder.SeedAsync();
	}
}

var screenManager = serviceProvider.GetRequiredService<ScreenManager>();
await screenManager.StartAsync(new ScreenData<MainScreen>());
