using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using apbd_tutorial09.Contracts;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Helpers;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

namespace apbd_tutorial09.Repositories.Implementation;

public class AppUserRepository: IAppUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AppUserRepository(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public void RegisterUser(RegisterUserRequest request)
    {
        var isUsernameExists = _dbContext.Users.Any(u => string.Equals(u.Username,request.Username,StringComparison.OrdinalIgnoreCase));
        if (isUsernameExists)
        {
            throw new UsernameAlreadyExists(request.Username);
        }
        var (hashedPassword, salt) = SecurityHelpers.GetHashedPasswordAndSalt(request.Password);

        var userToAdd = new AppUser
        {
            Username = request.Username,
            Password = hashedPassword,
            Salt = salt,
        };

        _dbContext.Users.Add(userToAdd);
        _dbContext.SaveChanges();
    }

    public (string accessToken, string refreshToken) LoginUser(LoginUserRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.Username);
        if (user == null)
        {
            throw new UserNotFound(request.Username);
        }
        
        var hashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(request.Password, user.Salt);
        if (user.Password != hashedPassword)
        {
            throw new InvalidPassword();
        }
        
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            new Claim(ClaimTypes.Name, $"{user.Username}")
        };
        
        var accessToken = GenerateAccessToken(userClaims);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(30);

        return (accessToken, refreshToken);
    }
    
    private string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var sskey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:SecretKey"]));
        var credentials = new SigningCredentials(sskey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Auth:ValidIssuer"],
            audience: _configuration["Auth:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public (JwtSecurityToken jwtToken, string refreshToken) GetNewAccessToken(RefreshTokenRequest requestToken)
    {
        AppUser user = _dbContext.Users.Where(u => u.RefreshToken == requestToken.RefreshToken).FirstOrDefault();

        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
        
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Id)),
            new Claim(ClaimTypes.Name, user.Username),
        };
        
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: _configuration["Auth:ValidIssuer"],
            audience: _configuration["Auth:ValidAudience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: creds
        );
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(30);
        _dbContext.SaveChanges();
        return (jwtToken,user.RefreshToken);
    }
}