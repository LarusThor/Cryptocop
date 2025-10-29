using Cryptocop.Models.Dtos;
using Cryptocop.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Cryptocop.Services.Implementations;

public class TokenService : ITokenService
{
    
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<string> GenerateJwtTokenAsync(UserDto user)
    {
        var key = _configuration["JWT:Key"];
        var issuer = _configuration["JWT:Issuer"];
        var audience = _configuration["JWT:Audience"];

        if (string.IsNullOrEmpty(key))
        {
            throw new Exception("JWT:Key not configured");
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);
        
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return await  Task.FromResult(jwt);
    }
}