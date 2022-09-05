namespace FridgeRegistry.WebAPI.Common.Configurations;

public class RedisCacheConfiguration
{
    public bool IsEnabled { get; set; }
    
    public string ConnectionString { get; set;  } 
}