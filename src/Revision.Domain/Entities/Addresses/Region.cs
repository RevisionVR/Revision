using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Addresses;

public class Region : Auditable
{
    public string Name { get; set; } = string.Empty;

    public long CountryId { get; set; }
    public Country Country { get; set; }
}