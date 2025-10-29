using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly ITokenRepository _tokenRepository;
    
    public JwtTokenService(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }
    
    public Task<bool> IsTokenBlacklistedAsync(int tokenId)
    {
        return _tokenRepository.IsTokenBlacklistedAsync(tokenId);    
    }
}