using FridgeRegistry.Identity.Contracts;
using FridgeRegistry.Identity.Contracts.Requests;
using FridgeRegistry.Identity.Contracts.Responses;
using FridgeRegistry.Identity.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.Identity.Controllers.V1;

[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost(ApiRoutes.Identity.Register)]
    public async Task<ActionResult> Register(UserRegistrationRequest request)
    {
        var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse()
            {
                ErrorMessages = authResponse.ErrorMessages,
            });
        }
        
        return Ok(new AuthSuccessResponse()
        {
            AccessToken = authResponse.AccessToken,
            RefreshToken = authResponse.RefreshToken,
        });
    }
    
    [HttpPost(ApiRoutes.Identity.Login)]
    public async Task<ActionResult> Login(UserLoginRequest request)
    {
        var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse()
            {
                ErrorMessages = authResponse.ErrorMessages,
            });
        }
        
        return Ok(new AuthSuccessResponse()
        {
            AccessToken = authResponse.AccessToken,
            RefreshToken = authResponse.RefreshToken,
        });
    }
    
    [HttpPost(ApiRoutes.Identity.Refresh)]
    public async Task<ActionResult> Refresh(RefreshTokenRequest request)
    {
        var authResponse = await _identityService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse()
            {
                ErrorMessages = authResponse.ErrorMessages,
            });
        }
        
        return Ok(new AuthSuccessResponse()
        {
            AccessToken = authResponse.AccessToken,
            RefreshToken = authResponse.RefreshToken,
        });
    }
    
    [HttpPost(ApiRoutes.Identity.Logout)]
    public async Task<ActionResult> Logout(LogoutRequest request)
    {
        var authResponse = await _identityService.Logout(request.AccessToken, request.RefreshToken);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse()
            {
                ErrorMessages = authResponse.ErrorMessages,
            });
        }
        
        return Ok();
    }
}