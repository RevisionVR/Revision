namespace Revision.Service.DTOs.Regions;

public class RegionCreationDto
{
    public string Name { get; set; } = string.Empty;
    public long CountryId { get; set; }
}