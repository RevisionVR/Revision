using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Addresses;

public class Address : Auditable
{
    public string Home { get; set; } = string.Empty;
    public int Description { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }

    public long CountryId { get; set; }
    public Country Country { get; set; }

    public long RegionId { get; set; }
    public Region Region { get; set; }

    public long DistrictId { get; set; }
    public District District { get; set; }
}