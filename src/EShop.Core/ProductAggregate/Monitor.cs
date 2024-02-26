namespace EShop.Core.ProductAggregate;

public class Monitor : ProductBase
{
    private int _refreshRate;
    private int _resolutionWidth;
    private int _resolutionHeight;

    public required int RefreshRate
    {
        get => _refreshRate;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Refresh rate must be greater than 0.");
            }

            _refreshRate = value;
        }
    }
    public required int ResolutionWidth
    {
        get => _resolutionWidth;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Resolution width must be greater than 0.");
            }

            _resolutionWidth = value;
        }
    }
    public required int ResolutionHeight
    {
        get => _resolutionHeight;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Resolution height must be greater than 0.");
            }

            _resolutionHeight = value;
        }
    }
}
