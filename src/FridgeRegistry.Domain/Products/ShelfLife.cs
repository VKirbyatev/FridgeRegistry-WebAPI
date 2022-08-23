using FridgeRegistry.Domain.Common;

namespace FridgeRegistry.Domain.Products;

public class ShelfLife : ValueObject
{
    public int LifeTime { get; private set; }
    
    public ShelfLife(int lifeTime)
    {
        LifeTime = lifeTime;
    }

    public DateTime GetExpirationDate()
    {
        return DateTime.Now.AddMilliseconds(LifeTime);
    }
    
    public DateTime GetExpirationDate(DateTime dateTime)
    {
        return dateTime.AddMilliseconds(LifeTime);
    }

    public override string ToString()
    {
        var timespan = DateTime.UtcNow.AddMilliseconds(LifeTime) - DateTime.UtcNow;
        return timespan.ToString(@"hh\:mm\:ss");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LifeTime;
    }
}