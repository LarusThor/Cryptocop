using Cryptocop.Models.Dtos;

namespace Cryptocop.Repositories.Interfaces;

public interface ITokenRepository
{
    Task<JwtTokenDto> CreateNewTokenAsync();
    Task<bool> IsTokenBlacklistedAsync(int tokenId);
    Task VoidTokenAsync(int tokenId);
}