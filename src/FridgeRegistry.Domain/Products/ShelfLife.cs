using FridgeRegistry.Domain.Common;

namespace FridgeRegistry.Domain.Products;

public class ShelfLife : ValueObject
{
    public uint LifeTime { get; private set; }
    
    public ShelfLife(uint lifeTime)
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
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LifeTime;
    }
}