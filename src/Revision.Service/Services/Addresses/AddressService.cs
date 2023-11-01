using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Addresses;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Addresses;

namespace Revision.Service.Services.Addresses;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Region> _regionRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<District> _districtRepository;
    public AddressService(
        IMapper mapper,
        IRepository<Region> regionRepository,
        IRepository<Country> countryRepository,
        IRepository<Address> addressRepository,
        IRepository<District> districtRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
        _countryRepository = countryRepository;
        _addressRepository = addressRepository;
        _districtRepository = districtRepository;
    }

    public async Task<Address> CreateAsync(AddressCreationDto dto)
    {
        var existCountry = await _countryRepository.SelectAsync(country => country.Id.Equals(dto.CountryId))
            ?? throw new RevisionException(404, "This country is not found");

        var existDistrict = await _districtRepository.SelectAsync(district => district.Id.Equals(dto.DistrictId))
            ?? throw new RevisionException(404, "This district is not found");

        var existRegion = await _regionRepository.SelectAsync(region => region.Id.Equals(dto.RegionId))
            ?? throw new RevisionException(404, "This region is not found");

        var mappedAddress = _mapper.Map<Address>(dto);
        mappedAddress.Region = existRegion;
        mappedAddress.Country = existCountry;
        mappedAddress.District = existDistrict;
        mappedAddress.CreatedAt = TimeHelper.GetDateTime();

        await _addressRepository.AddAsync(mappedAddress);
        await _addressRepository.SaveAsync();

        return mappedAddress;
    }

    public async Task<Address> UpdateAsync(long id, AddressUpdateDto dto)
    {
        var existAddress = await _addressRepository.SelectAsync(address => address.Id.Equals(id),
            includes: new[] { "Educations" })
            ?? throw new RevisionException(404, "This address is not found");

        var existCountry = await _countryRepository.SelectAsync(country => country.Id.Equals(dto.CountryId))
            ?? throw new RevisionException(404, "This country is not found");

        var existDistrict = await _districtRepository.SelectAsync(district => district.Id.Equals(dto.DistrictId))
            ?? throw new RevisionException(404, "This district is not found");

        var existRegion = await _regionRepository.SelectAsync(region => region.Id.Equals(dto.RegionId))
            ?? throw new RevisionException(404, "This region is not found");

        var mappedAddress = _mapper.Map(dto, existAddress);
        mappedAddress.Id = id;
        mappedAddress.Region = existRegion;
        mappedAddress.Country = existCountry;
        mappedAddress.District = existDistrict;
        mappedAddress.UpdatedAt = TimeHelper.GetDateTime();

        _addressRepository.Update(mappedAddress);
        await _addressRepository.SaveAsync();

        return mappedAddress;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAddress = await _addressRepository.SelectAsync(address => address.Id.Equals(id))
            ?? throw new RevisionException(404, "This address is not found");

        _addressRepository.Delete(existAddress);
        await _addressRepository.SaveAsync();
        return true;
    }

    public async Task<AddressResultDto> GetByIdAsync(long id)
    {
        var existAddress = await _addressRepository.SelectAsync(address => address.Id.Equals(id),
            includes: new[] { "Educations" })
            ?? throw new RevisionException(404, "This address is not found");

        return _mapper.Map<AddressResultDto>(existAddress);
    }

    public async Task<IEnumerable<AddressResultDto>> GetAllAsync()
    {
        var addesses = await _addressRepository.SelectAll(includes: new[] { "Educations" })
            .ToListAsync();

        return _mapper.Map<IEnumerable<AddressResultDto>>(addesses);
    }
}