using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Helpers;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly CryptocopDbContext  _dbcontext;
    
    private readonly ITokenRepository _tokenRepository;

    public UserRepository(CryptocopDbContext dbContext, ITokenRepository tokenRepository)
    {
        _dbcontext = dbContext;
        _tokenRepository = tokenRepository;
    }

    public async Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(u => u.Email == inputModel.Email);

        if (user != null)
        {
            throw new Exception("Email is already taken");
        }

        var newUser = new User
        {
            FullName = inputModel.FullName,
            Email = inputModel.Email,
            HashedPassword = HashingHelper.HashPassword(inputModel.Password),

        };

        if (inputModel.PasswordConfirmation != inputModel.Password)
        {
            throw new Exception("Passwords do not match");
        }
        
        _dbcontext.Users.Add(newUser);
        await _dbcontext.SaveChangesAsync();
        
        var token = await _tokenRepository.CreateNewTokenAsync();

        return new UserDto
        {
            Id = newUser.Id,
            FullName = newUser.FullName,
            Email = newUser.Email,
            TokenId = token.Id
        };
    }

    public async Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        var user =  await _dbcontext.Users
            .FirstOrDefaultAsync(u => u.Email == loginInputModel.Email);

        if (user == null)
        {
            throw new Exception("Email is invalid");
        }
        
        var hashedInputPassword =  HashingHelper.HashPassword(loginInputModel.Password);

        if (user.HashedPassword != hashedInputPassword)
        {
            throw new Exception("Password does not match a user");
        }

        var token = await _tokenRepository.CreateNewTokenAsync();

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            TokenId = token.Id
        };
    }
}