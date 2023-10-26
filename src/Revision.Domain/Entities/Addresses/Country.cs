using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Addresses;

public class Country : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}
