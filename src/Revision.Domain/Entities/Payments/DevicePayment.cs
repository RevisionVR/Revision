using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Payments;

public class DevicePayment : Auditable
{
    public decimal Price { get; set; } 
    public decimal TotalPrice { get; set; }
    public int DeviceCount { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}
