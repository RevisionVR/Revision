using Revision.Service.DTOs.Countries;

namespace Revision.Service.Interfaces.Addresses;

public interface ICountryService
{
    // Task<CountryResultDto> CreateAsync(CountryCreationDto dto);
    Task<bool> SetAsync();
    Task<CountryResultDto> GetByIdAsync(long id);
    Task<IEnumerable<CountryResultDto>> GetAllAsync();
}