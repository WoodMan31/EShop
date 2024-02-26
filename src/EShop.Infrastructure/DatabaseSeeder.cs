using EShop.Core.ProductAggregate;
using EShop.Infrastructure;
using Monitor = EShop.Core.ProductAggregate.Monitor;

namespace EShop.Infrastructure;

public sealed class DatabaseSeeder
{
	private readonly DatabaseContext _databaseContext;

	public DatabaseSeeder(DatabaseContext databaseContext)
    {
		_databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
	}

	public async Task SeedAsync()
	{
		_databaseContext.Products.Add(new ComputerMouse()
		{
			Name = "Logitech MX Master 3",
			Price = 99.99m,
			AvailableQuantity = 10,
			ConnectionType = "Bluetooth",
			Description = "The Logitech MX Master 3 is an advanced wireless mouse that lets you work comfortably anywhere.",
			Dpi = 4000,
			SensorType = "Laser",
			Weight = 141
		});

		_databaseContext.Products.Add(new ComputerMouse()
		{
			Name = "Razer DeathAdder V2",
			Price = 69.99m,
			AvailableQuantity = 5,
			ConnectionType = "USB",
			Description = "The Razer DeathAdder V2 is a wired gaming mouse that features Razer's Focus+ 20K DPI Optical Sensor.",
			Dpi = 20000,
			SensorType = "Optical",
			Weight = 82
		});

		_databaseContext.Products.Add(new Monitor()
		{
			Name = "LG 27GL850-B",
			Price = 499.99m,
			AvailableQuantity = 3,
			RefreshRate = 144,
			ResolutionWidth = 2560,
			ResolutionHeight = 1440,
			Description = "The LG 27GL850-B is a 27-inch gaming monitor with a 144Hz refresh rate and a 1ms response time."
		});

		_databaseContext.Products.Add(new Monitor()
		{
			Name = "ASUS TUF Gaming VG27AQ",
			Price = 399.99m,
			AvailableQuantity = 2,
			RefreshRate = 165,
			ResolutionWidth = 2560,
			ResolutionHeight = 1440,
			Description = "The ASUS TUF Gaming VG27AQ is a 27-inch gaming monitor with a 165Hz refresh rate and a 1ms response time."
		});

		await _databaseContext.SaveChangesAsync();
	}
}
