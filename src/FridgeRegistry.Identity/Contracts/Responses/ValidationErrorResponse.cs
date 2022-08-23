using FridgeRegistry.Identity.Models;

namespace FridgeRegistry.Identity.Contracts.Responses;

public class ValidationErrorResponse
{
    public List<ValidationErrorModel> Errors { get; set; } = new List<ValidationErrorModel>();
}