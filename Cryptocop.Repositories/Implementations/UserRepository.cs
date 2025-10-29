using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Repositories.Implementations;

public class UserRepository
{
    public Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        throw new NotImplementedException();
    }
}