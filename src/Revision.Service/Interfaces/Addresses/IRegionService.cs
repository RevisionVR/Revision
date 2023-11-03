using Revision.Service.DTOs.Regions;

namespace Revision.Service.Interfaces.Addresses;

public interface IRegionService
{
    //Task<RegionResultDto> CreateAsync(RegionCreationDto dto);
    Task<bool> SetAsync();
    Task<RegionResultDto> GetByIdAsync(long id);
    Task<IEnumerable<RegionResultDto>> GetAllAsync();
}