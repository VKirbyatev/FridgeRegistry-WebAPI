namespace FridgeRegistry.WebAPI.Contracts.Responses;

public class HealthCheckResponse
{
    public string Status { get; set; }  = null!;
    
    public IEnumerable<HealthCheck> Checks { get; set; } = null!;
    
    public TimeSpan Duration { get; set; }
}