using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Implementations;

public class AddressRepository : IAddressRepository
{
    private readonly CryptocopDbContext _dbcontext;

    public AddressRepository(CryptocopDbContext dbContext)
    {
        _dbcontext = dbContext;
    }
    
    public async Task AddAddressAsync(string email, AddressInputModel address)
    {
        var user = _dbcontext.Users
            .FirstOrDefault(user => user.Email == email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var addr = new Address
        {
            UserId = user.Id,
            StreetName = address.StreetName,
            HouseNumber = address.HouseNumber,
            ZipCode = address.ZipCode,
            Country = address.Country,
            City = address.City,
        };
        _dbcontext.Addresses.Add(addr);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email)
    {
        var user = _dbcontext.Users
            .FirstOrDefault(user => user.Email == email);
        
        if (user == null)
        {
            return Enumerable.Empty<AddressDto>();
        }

        var Addresses = _dbcontext.Addresses
            .Where(address => address.UserId == user.Id)
            .Select(o => new AddressDto()
            {
                Id =  o.Id,
                StreetName = o.StreetName,
                HouseNumber = o.HouseNumber,
                ZipCode = o.ZipCode,
                Country = o.Country,
                City = o.City,
            });
        return await Addresses.ToListAsync();
    }

    public async Task DeleteAddressAsync(string email, int addressId)
    {
        var user = _dbcontext.Users
            .FirstOrDefault(user => user.Email == email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var entity = await _dbcontext.Addresses
            .FirstOrDefaultAsync(address => address.Id == addressId && address.UserId == user.Id);
        
        if (entity != null)
        {
            _dbcontext.Addresses.Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Address not found");
        }
    }
}