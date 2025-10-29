using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Repositories.Interfaces;

public interface IAddressRepository
{
    Task AddAddressAsync(string email, AddressInputModel address);
    Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email);
    Task DeleteAddressAsync(string email, int addressId);
}