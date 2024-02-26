namespace EShop.Core;

public interface IIdentifiable<TKey>
{
    public TKey Id { get; }
}
