namespace Revision.Service.DTOs.Districts;

public class DistrictCreationDto
{
    public string Name { get; set; } = string.Empty;
    public long RegionId { get; set; }
}