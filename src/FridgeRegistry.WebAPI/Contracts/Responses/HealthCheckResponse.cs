namespace FridgeRegistry.WebAPI.Contracts.Responses;

public class HealthCheckResponse
{
    public string Status { get; set; }
    
    public IEnumerable<HealthCheck> Checks { get; set; }
    
    public TimeSpan Duration { get; set; }
}