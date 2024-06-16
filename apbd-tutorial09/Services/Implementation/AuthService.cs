using System.IdentityModel.Tokens.Jwt;
using apbd_tutorial09.Contracts;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;

namespace apbd_tutorial09.Services.Implementation;

public class AuthService: IAuthService
{
    private readonly IAppUserRepository _appUserRepository;

    public AuthService(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public void RegisterUser(RegisterUserRequest request)
    {
        _appUserRepository.RegisterUser(request);
    }

    public (string accessToken, string refreshToken) LoginUser(LoginUserRequest request)
    {
        return _appUserRepository.LoginUser(request);
    }

    public (JwtSecurityToken jwtToken, string refreshToken) GetNewAccessToken(RefreshTokenRequest requestToken)
    {
        return _appUserRepository.GetNewAccessToken(requestToken);
    }
}