using System.Security.Claims;

namespace FridgeRegistry.WebAPI.Common.Extensions.HttpExtensions;

public static class GetUserIdExtension
{
    public static string GetUserId(this HttpContext httpContext)
    {
        if (httpContext.User == null)
        {
            return string.Empty;
        }

        return httpContext.User.Claims.Single(claim => claim.Type == "id").Value;
    }
}