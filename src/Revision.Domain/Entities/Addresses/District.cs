using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Addresses;

public class District : Auditable
{
    public string Name { get; set; } = string.Empty;

    public long RegionId { get; set; }
    public Region Region { get; set; }
}
