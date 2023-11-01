using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Models;
using Revision.Service.DTOs.Districts;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Addresses;
using Revision.Shared.Helpers;

namespace Revision.Service.Services.Addresses;

public class DistrictService : IDistrictService
{
    private readonly IMapper _mapper;
    private readonly IRepository<District> _districtRepository;
    public DistrictService(IMapper mapper, IRepository<District> districtRepository)
    {
        _mapper = mapper;
        _districtRepository = districtRepository;
    }

    public async Task<DistrictResultDto> CreateAsync(DistrictCreationDto dto)
    {
        var existDistrict = await _districtRepository.SelectAsync(district => district.Name.Equals(dto.Name)
        && district.RegionId.Equals(dto.RegionId));
        if (existDistrict is not null)
            throw new RevisionException(403, "This district already exists");

        var mappedDistrict = _mapper.Map<District>(dto);
        mappedDistrict.CreatedAt = TimeHelper.GetDateTime();
        await _districtRepository.AddAsync(mappedDistrict);
        await _districtRepository.SaveAsync();

        return _mapper.Map<DistrictResultDto>(mappedDistrict);
    }

    public async Task<bool> SetAsync()
    {
        var dbSource = _districtRepository.SelectAll();
        if (dbSource.Any())
            throw new RevisionException(403, "Districts already exist");

        string path = EnvironmentHelper.DistrictPath;
        var source = File.ReadAllText(path);
        var districts = JsonConvert.DeserializeObject<IEnumerable<DistrictModel>>(source);

        foreach (var district in districts)
        {
            var mappedDistrict = _mapper.Map<District>(district);
            mappedDistrict.CreatedAt = TimeHelper.GetDateTime();

            await _districtRepository.AddAsync(mappedDistrict);
            await _districtRepository.SaveAsync();
        }
        return true;
    }

    public async Task<DistrictResultDto> GetByIdAsync(long id)
    {
        var existDistrict = await _districtRepository.SelectAsync(district => district.Id.Equals(id),
            includes: new[] { "Region.Country" })
            ?? throw new RevisionException(404, "This district is not found");

        return _mapper.Map<DistrictResultDto>(existDistrict);
    }

    public async Task<IEnumerable<DistrictResultDto>> GetAllAsync()
    {
        var districts = await _districtRepository.SelectAll(includes: new[] { "Region.Country" })
            .ToListAsync();
        return _mapper.Map<IEnumerable<DistrictResultDto>>(districts);
    }
}