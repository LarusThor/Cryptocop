using Cryptocop.Models.Dtos;

namespace Cryptocop.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(UserDto user);
}