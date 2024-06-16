using System.IdentityModel.Tokens.Jwt;
using apbd_tutorial09.Contracts;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Repositories.Abstraction;

public interface IAppUserRepository
{
    void RegisterUser(RegisterUserRequest request);
    (string accessToken, string refreshToken) LoginUser(LoginUserRequest request);
    (JwtSecurityToken jwtToken, string refreshToken) GetNewAccessToken(RefreshTokenRequest requestToken);
}