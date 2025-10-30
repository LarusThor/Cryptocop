using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Services.Interfaces;

public interface IAccountService
{
    Task<UserDto?> CreateUserAsync(RegisterInputModel inputModel);
    Task<UserDto?> AuthenticateUserAsync(LoginInputModel loginInputModel);
    Task LogoutAsync(int tokenId);
}