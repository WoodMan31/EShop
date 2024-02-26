namespace EShop.Core.ProductAggregate;

public class ComputerMouse : ProductBase
{
    private int _dpi;
    private string _connectionType = default!;
    private string _sensorType = default!;
    private int _weight;

    public required int Dpi
    {
        get => _dpi;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "DPI must be greater than 0.");
            }

            _dpi = value;
        }
    }
    public required string ConnectionType
    {
        get => _connectionType;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Connection type must not be null or empty.");
            }

            _connectionType = value;
        }
    }
    public required string SensorType
    {
        get => _sensorType;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Sensor type must not be null or empty.");
            }

            _sensorType = value;
        }
    }
    public required int Weight
    {
        get => _weight;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Weight must be greater than 0.");
            }

            _weight = value;
        }
    }
}
