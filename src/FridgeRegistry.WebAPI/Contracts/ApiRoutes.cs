namespace FridgeRegistry.WebAPI.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";
    
    public const string Version = "v1";

    public const string Base = $"{Root}/{Version}";
    
    public static class Category
    {
        public const string GetList = Base + "/category";
        
        public const string GetDescription = Base + "/category/{id}";
        
        public const string Create = Base + "/category";
        
        public const string Update = Base + "/category/{id}";
        
        public const string Remove = Base + "/category/{id}";
    }
}