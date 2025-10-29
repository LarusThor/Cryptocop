using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserDto> CreateUserAsync(RegisterInputModel inputModel);
    Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel);
}