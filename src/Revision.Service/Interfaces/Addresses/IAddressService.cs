using Revision.Domain.Entities.Addresses;
using Revision.Service.DTOs.Addresses;

namespace Revision.Service.Interfaces.Addresses;

public interface IAddressService
{
    Task<Address> CreateAsync(AddressCreationDto dto);
    Task<Address> UpdateAsync(long id, AddressUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}