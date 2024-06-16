using apbd_tutorial09.Contracts;
using apbd_tutorial09.Models;
using apbd_tutorial09.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial09.Controller;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    {
        _authService.RegisterUser(request);
        return Ok();
    }
    
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginUserRequest request)
    {
        var (accessToken, refreshToken) = _authService.LoginUser(request);
        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequest requestToken)
    {
        var (jwtToken, refreshToken) = _authService.GetNewAccessToken(requestToken);
        return Ok(new
        {
            AccessToken = jwtToken,
            RefreshToken = refreshToken
        });
    }
}