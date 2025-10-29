using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Services.Interfaces;

public interface IAddressService
{
    Task AddAddressAsync(string email, AddressInputModel address);
    Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email);
    Task DeleteAddressAsync(string email, int addressId);
}