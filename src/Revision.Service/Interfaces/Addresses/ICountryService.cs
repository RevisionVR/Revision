using Revision.Service.DTOs.Countries;

namespace Revision.Service.Interfaces.Addresses;

public interface ICountryService
{
    Task<bool> SetAsync();
    Task<CountryResultDto> GetByIdAsync(long id);
    Task<IEnumerable<CountryResultDto>> GetByIdAsync();
}