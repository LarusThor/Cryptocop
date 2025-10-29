using Cryptocop.Models.Dtos;

namespace Cryptocop.Repositories.Implementations;

public class TokenRepository
{
    public Task<JwtTokenDto> CreateNewTokenAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsTokenBlacklistedAsync(int tokenId)
    {
        throw new NotImplementedException();
    }

    public Task VoidTokenAsync(int tokenId)
    {
        throw new NotImplementedException();
    }
}