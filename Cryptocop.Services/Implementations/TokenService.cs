using Cryptocop.Models.Dtos;
using Cryptocop.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cryptocop.Repositories.Implementations;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cryptocop.Services.Implementations;

public class TokenService : ITokenService
{
    
    private readonly IConfiguration _configuration;
    private readonly ITokenRepository _tokenRepository;

    public TokenService(IConfiguration configuration, ITokenRepository tokenRepository)
    {
        _configuration = configuration;
        _tokenRepository = tokenRepository;
    }
    
    public async Task<string> GenerateJwtTokenAsync(UserDto user)
    {
        var key = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        if (string.IsNullOrEmpty(key))
        {
            throw new Exception("Jwt:Key not configured");
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var newToken = await _tokenRepository.CreateNewTokenAsync();
        
        var claims = new[]
        {
            new Claim("email", user.Email),
            new Claim("id", user.Id.ToString()),
            new Claim("tokenId", newToken.Id.ToString()), 
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