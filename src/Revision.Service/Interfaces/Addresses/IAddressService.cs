using Revision.Domain.Entities.Addresses;
using Revision.Service.DTOs.Addresses;

namespace Revision.Service.Interfaces.Addresses;

public interface IAddressService
{
    Task<AddressResultDto> CreateAsync(AddressCreationDto dto);
    Task<AddressResultDto> UpdateAsync(long id, AddressUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<AddressResultDto> GetByIdAsync(long id);
    Task<IEnumerable<AddressResultDto>> GetAllAsync();
}