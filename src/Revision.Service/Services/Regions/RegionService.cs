using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Addresses;
using Revision.Service.DTOs.Regions;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Addresses;

namespace Revision.Service.Services.Regions;

public class RegionService : IRegionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Region> _regionRepository;
    public RegionService(IMapper mapper, IRepository<Region> regionRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
    }

    public async Task<bool> SetAsync()
    {
        var dbSource = _regionRepository.SelectAll();
        if (dbSource.Any())
            throw new RevisionException(403, "Regions already exist");

        string path = "";
        var source = File.ReadAllText(path);
        var regions = JsonConvert.DeserializeObject<IEnumerable<RegionCreationDto>>(source);

        foreach (var region in regions)
        {
            var mappedRegion = _mapper.Map<Region>(region);
            await _regionRepository.AddAsync(mappedRegion);
            await _regionRepository.SaveAsync();
        }
        return true;
    }

    public async Task<RegionResultDto> GetByIdAsync(long id)
    {
        var region = await _regionRepository.SelectAsync(r => r.Id.Equals(id), includes: new[] { "Country" })
            ?? throw new RevisionException(404, "This region is not found");

        return _mapper.Map<RegionResultDto>(region);
    }

    public async Task<IEnumerable<RegionResultDto>> GetAllAsync()
    {
        var regions = await _regionRepository.SelectAll(includes: new[] { "Country" }).ToListAsync();
        return _mapper.Map<IEnumerable<RegionResultDto>>(regions);
    }
}
