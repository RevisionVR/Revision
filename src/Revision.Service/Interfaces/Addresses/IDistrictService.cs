using Revision.Service.DTOs.Districts;

namespace Revision.Service.Interfaces.Addresses;

public interface IDistrictService
{
    Task<bool> SetAsync();
    Task<DistrictResultDto> GetByIdAsync(long id);
    Task<IEnumerable<DistrictResultDto>> GetAllAsync();
}