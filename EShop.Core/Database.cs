using EShop.Core.Interfaces;

namespace EShop.Core;

public sealed class Database
{
    public List<IUser> Users { get; } = new();
    public List<IProduct> Products { get; } = new();

    public void Save(string fileName)
    {
        throw new NotImplementedException();
    }

    public void Load(string fileName)
    {
        throw new NotImplementedException();
    }
}
