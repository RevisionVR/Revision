namespace Revision.Service.DTOs.Addresses;

public class AddressCreationDto
{
    public string Home { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public long CountryId { get; set; }
    public long RegionId { get; set; }
    public long DistrictId { get; set; }
}