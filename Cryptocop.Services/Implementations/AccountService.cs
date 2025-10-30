using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository  _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtTokenService _jwtTokenService;
    public AccountService(IUserRepository userRepository, ITokenRepository tokenRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _jwtTokenService =  jwtTokenService;
    }
    
    public Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        return _userRepository.CreateUserAsync(inputModel);
    }

    public async Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        var user = await _userRepository.AuthenticateUserAsync(loginInputModel);
        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        var newToken = await _tokenRepository.CreateNewTokenAsync();
        user.TokenId = newToken.Id;
        return user;
    }

    public Task LogoutAsync(int tokenId)
    {
        return _tokenRepository.VoidTokenAsync(tokenId);
    }
}