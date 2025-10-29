using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository  _userRepository;
    private readonly ITokenRepository _tokenRepository;
    
    public AccountService(IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
    }
    
    public Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        return _userRepository.CreateUserAsync(inputModel);
    }

    public Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        return _userRepository.AuthenticateUserAsync(loginInputModel);
    }

    public Task LogoutAsync(int tokenId)
    {
        return _tokenRepository.VoidTokenAsync(tokenId);
    }
}