namespace FridgeRegistry.WebAPI.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";
    
    public const string Version = "v1";

    public const string Base = $"{Root}/{Version}";
    
    public static class Category
    {
        public const string GetList = Base + "/category";
        
        public const string GetProducts = Base + "/category/{id}/products";
        
        public const string GetDescription = Base + "/category/{id}";
        
        public const string Create = Base + "/category";
        
        public const string Update = Base + "/category/{id}";
        
        public const string AddProduct = Base + "/category/{categoryId}/add-product/{productId}";
        
        public const string Remove = Base + "/category/{id}";
    }

    public static class Product
    {
        public const string GetList = Base + "/product";

        public const string GetDescription = Base + "/product/{id}";
        
        public const string Create = Base + "/product";
        
        public const string Update = Base + "/product/{id}";

        public const string Remove = Base + "/product/{id}";
    }
    
    public static class UserProducts
    {
        public const string GetList = Base + "/user/product";

        public const string GetDescription = Base + "/user/product/{id}";
        
        public const string Create = Base + "/user/product";
        
        public const string Update = Base + "/user/product/{id}";

        public const string Remove = Base + "/user/product/{id}";
    }
}