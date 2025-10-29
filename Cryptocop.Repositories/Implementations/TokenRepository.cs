using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Implementations;

public class TokenRepository : ITokenRepository
{
    private readonly CryptocopDbContext _dbcontext;

    public TokenRepository(CryptocopDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task<JwtTokenDto> CreateNewTokenAsync()
    {
        var newToken = new JwtToken
        {
            Blacklisted = false
        };
        
        _dbcontext.JwtTokens.Add(newToken);
        await _dbcontext.SaveChangesAsync();

        return new JwtTokenDto
        {
            Id = newToken.Id,
            Blacklisted = newToken.Blacklisted,
        };
    }

    public async Task<bool> IsTokenBlacklistedAsync(int tokenId)
    {
        var token = await  _dbcontext.JwtTokens
            .FirstOrDefaultAsync(t => t.Id == tokenId);

        if (token == null)
        {
            throw new Exception("Token not found");
        }

        if (token.Blacklisted)
        {
            return true;
        } 
        return false;
    }

    public async Task VoidTokenAsync(int tokenId)
    {
        var token = await  _dbcontext.JwtTokens
            .FirstOrDefaultAsync(t => t.Id == tokenId);

        if (token == null)
        {
            throw new Exception("Token not found");
        }
        
        token.Blacklisted = true;
        await _dbcontext.SaveChangesAsync();
    }
}