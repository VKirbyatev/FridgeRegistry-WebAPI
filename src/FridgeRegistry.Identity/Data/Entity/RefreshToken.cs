using System.ComponentModel.DataAnnotations;

namespace FridgeRegistry.Identity.Data.Entity;

public class RefreshToken
{
    public Guid TokenId { get; set; }
    public string JwtId { get; set; }
    
    public string UserId { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime ExpiryDate { get; set; }
    
    public bool IsUsed { get; set; }

    public bool IsInvalidated { get; set; }
}