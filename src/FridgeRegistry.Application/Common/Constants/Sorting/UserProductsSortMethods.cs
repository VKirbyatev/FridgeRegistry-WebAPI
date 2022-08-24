namespace FridgeRegistry.Application.Common.Constants.Sorting;

public static class UserProductsSortMethods
{
    public static readonly string[] AvailableSortMethods = new[] { ByName, ByExpirationDate };

    public const string ByName = "NAME";
        
    public const string ByExpirationDate = "EXPIRATION_DATE";
}