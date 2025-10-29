using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class AddressService : IAddressService
{
    private readonly  IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public Task AddAddressAsync(string email, AddressInputModel address)
    {
        return  _addressRepository.AddAddressAsync(email, address);
    }

    public Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email)
    {
        return  _addressRepository.GetAllAddressesAsync(email);
    }

    public Task DeleteAddressAsync(string email, int addressId)
    {
        return  _addressRepository.DeleteAddressAsync(email, addressId);
    }
}