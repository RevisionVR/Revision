using Revision.Service.DTOs.Regions;

namespace Revision.Service.DTOs.Districts;

public class DistrictResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RegionResultDto Region { get; set; }
}