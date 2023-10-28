using AutoMapper;
using Newtonsoft.Json;
using Revision.Service.Exceptions;
using Revision.Service.DTOs.Countries;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.Interfaces.Addresses;
using Microsoft.EntityFrameworkCore;

namespace Revision.Service.Services.Countries;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Country> _countryRepository;
    public CountryService(IMapper mapper, IRepository<Country> countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    public async Task<bool> SetAsync()
    {
        var dbSource = _countryRepository.SelectAll();
        if (dbSource.Any())
            throw new RevisionException(403,"Countries already exist");

        string path = "";
        var source = File.ReadAllText(path);
        var countries = JsonConvert.DeserializeObject<IEnumerable<CountryCreationDto>>(source);

        foreach (var country in countries)
        {
            var mappedCountry = _mapper.Map<Country>(country);
            await _countryRepository.AddAsync(mappedCountry);
            await _countryRepository.SaveAsync();
        }
        return true;
    }
    
    public async Task<CountryResultDto> GetByIdAsync(long id)
    {
        var country = await _countryRepository.SelectAsync(r => r.Id.Equals(id))
            ?? throw new RevisionException(404, "This country is not found");

        return _mapper.Map<CountryResultDto>(country);
    }

    public async Task<IEnumerable<CountryResultDto>> GetAllAsync()
    {
        var countries = await _countryRepository.SelectAll().ToListAsync();
        return _mapper.Map<IEnumerable<CountryResultDto>>(countries);
    }
}
