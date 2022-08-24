namespace FridgeRegistry.Application.Common.Query;

public static class SortQueryParams
{
    public const string Asc = "ASC";
    
    public const string Desc = "DESC";
    
    public static class UserProduct
    {
        public static readonly string[] AvailableSortMethods = new[] { ByName, ByExpirationDate };

        public const string ByName = "NAME";
        
        public const string ByExpirationDate = "EXPIRATION_DATE";
    }
}