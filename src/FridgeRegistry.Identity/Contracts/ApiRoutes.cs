namespace FridgeRegistry.Identity.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";
    
    public const string Version = "v1";

    public const string Base = $"{Root}/{Version}";
    
    public static class Identity
    {
        public const string Register = $"{Base}/register";
        
        public const string Login = $"{Base}/login";
        
        public const string Refresh = $"{Base}/refresh";
        
        public const string Logout = $"{Base}/logout";
    }
}