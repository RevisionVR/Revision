using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Payments;

public class DevicePayment : Auditable
{
    public decimal Price { get; set; }
    public DateTime LastDate { get; set; }
    public DateTime NextDate { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}