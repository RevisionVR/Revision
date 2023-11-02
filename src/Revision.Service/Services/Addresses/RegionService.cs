using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Models;
using Revision.Service.DTOs.Regions;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Validations.Addresses.Regions;
using Revision.Shared.Helpers;

namespace Revision.Service.Services.Addresses;

public class RegionService : IRegionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Region> _regionRepository;
    public RegionService(IMapper mapper, IRepository<Region> regionRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
    }

    public async Task<RegionResultDto> CreateAsync(RegionCreationDto dto)
    {
        var validation = new RegionCreationDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            throw new RevisionException(400, result.Errors.FirstOrDefault().ToString());

        var existRegion = await _regionRepository.SelectAsync(region => region.Name.Equals(dto.Name) && region.CountryId.Equals(dto.CountryId));
        if (existRegion is not null)
            throw new RevisionException(403, "This region already exists");

        var mappedRegion = _mapper.Map<Region>(dto);
        mappedRegion.CreatedAt = TimeHelper.GetDateTime();
        await _regionRepository.AddAsync(mappedRegion);
        await _regionRepository.SaveAsync();

        return _mapper.Map<RegionResultDto>(mappedRegion);
    }

    public async Task<bool> SetAsync()
    {
        var dbSource = _regionRepository.SelectAll();
        if (dbSource.Any())
            throw new RevisionException(403, "Regions already exist");

        string path = EnvironmentHelper.RegionPath;
        var source = File.ReadAllText(path);
        var regions = JsonConvert.DeserializeObject<IEnumerable<RegionModel>>(source);

        foreach (var region in regions)
        {
            var mappedRegion = _mapper.Map<Region>(region);
            mappedRegion.CreatedAt = TimeHelper.GetDateTime();

            await _regionRepository.AddAsync(mappedRegion);
            await _regionRepository.SaveAsync();
        }
        return true;
    }

    public async Task<RegionResultDto> GetByIdAsync(long id)
    {
        var existRegion = await _regionRepository.SelectAsync(region => region.Id.Equals(id),
            includes: new[] { "Country" })
            ?? throw new RevisionException(404, "This region is not found");

        return _mapper.Map<RegionResultDto>(existRegion);
    }

    public async Task<IEnumerable<RegionResultDto>> GetAllAsync()
    {
        var regions = await _regionRepository.SelectAll(includes: new[] { "Country" }).ToListAsync();
        return _mapper.Map<IEnumerable<RegionResultDto>>(regions);
    }
}
