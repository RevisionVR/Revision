using Revision.Service.DTOs.Countries;
using Revision.Service.DTOs.Districts;

namespace Revision.Service.Interfaces.Addresses;

public interface IDistrictService
{
    Task<DistrictResultDto> CreateAsync(DistrictCreationDto dto);
    Task<bool> SetAsync();
    Task<DistrictResultDto> GetByIdAsync(long id);
    Task<IEnumerable<DistrictResultDto>> GetAllAsync();
}