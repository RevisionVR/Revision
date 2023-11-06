using Revision.Domain.Commons;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentResultDto : Auditable
{
    public decimal TotalPrice { get; set; }
    public int Count { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public EducationResultDto Education { get; set; }
}