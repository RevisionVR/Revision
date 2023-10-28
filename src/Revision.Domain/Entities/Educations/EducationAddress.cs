using Revision.Domain.Commons;
using Revision.Domain.Entities.Addresses;

namespace Revision.Domain.Entities.Educations;

public class EducationAddress : Auditable
{
    public long EducationId { get; set; }
    public Education Education { get; set; }

    public long AddressId { get; set; }
    public Address Address { get; set; }
}