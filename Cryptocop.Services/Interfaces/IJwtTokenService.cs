namespace Cryptocop.Services.Interfaces;

public interface IJwtTokenService
{
    Task<bool> IsTokenBlacklistedAsync(int tokenId);
}