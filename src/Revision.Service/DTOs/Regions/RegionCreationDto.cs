namespace Revision.Service.DTOs.Regions;

public class RegionCreationDto
{
    public long CountryId { get; set; }
    public string Name { get; set; } = string.Empty;
}