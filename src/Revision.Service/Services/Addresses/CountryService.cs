using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Models;
using Revision.Service.DTOs.Countries;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Addresses;
using Revision.Shared.Helpers;

namespace Revision.Service.Services.Addresses;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Country> _countryRepository;
    public CountryService(IMapper mapper, IRepository<Country> countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    //public async Task<CountryResultDto> CreateAsync(CountryCreationDto dto)
    //{
    //    var existCountry = await _countryRepository.SelectAsync(country =>
    //    country.Name.ToLower().Equals(dto.Name.ToLower()) || 
    //    country.CountryCode.ToLower().Equals(dto.CountryCode.ToLower()));
    //    if (existCountry is not null)
    //        throw new RevisionException(403, "This country already exists");

    //    var mappedCountry = _mapper.Map<Country>(dto);
    //    mappedCountry.CreatedAt = TimeHelper.GetDateTime();
    //    await _countryRepository.AddAsync(mappedCountry);
    //    await _countryRepository.SaveAsync();

    //    return _mapper.Map<CountryResultDto>(mappedCountry);
    //}

    public async Task<bool> SetAsync()
    {
        var dbSource = _countryRepository.SelectAll();
        if (dbSource.Any())
            throw new RevisionException(403, "Countries already exist");

        string path = EnvironmentHelper.CountryPath;
        var source = File.ReadAllText(path);
        var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(source);

        foreach (var country in countries)
        {
            var mappedCountry = _mapper.Map<Country>(country);
            mappedCountry.CreatedAt = TimeHelper.GetDateTime();
            mappedCountry.UpdatedAt = TimeHelper.GetDateTime();

            await _countryRepository.AddAsync(mappedCountry);
            await _countryRepository.SaveAsync();
        }

        return true;
    }

    public async Task<CountryResultDto> GetByIdAsync(long id)
    {
        var existCountry = await _countryRepository.SelectAsync(country => country.Id.Equals(id))
            ?? throw new RevisionException(404, "This country is not found");

        return _mapper.Map<CountryResultDto>(existCountry);
    }

    public async Task<IEnumerable<CountryResultDto>> GetAllAsync()
    {
        var countries = await _countryRepository.SelectAll().ToListAsync();
        return _mapper.Map<IEnumerable<CountryResultDto>>(countries);
    }
}
