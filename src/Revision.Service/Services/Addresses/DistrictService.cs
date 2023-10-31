using AutoMapper;
using Newtonsoft.Json;
using Revision.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Revision.Service.DTOs.Districts;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Commons.Helpers;
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

    public async Task<bool> SetAsync()
    {
        var dbSource = _districtRepository.SelectAll();
        if (dbSource.Any())
            throw new RevisionException(403, "Districts already exist");

        string path = EnvironmentHelper.DistrictPath;
        var source = File.ReadAllText(path);
        var districts = JsonConvert.DeserializeObject<IEnumerable<DistrictCreationDto>>(source);

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