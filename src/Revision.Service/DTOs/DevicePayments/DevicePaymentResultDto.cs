using Revision.Domain.Commons;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentResultDto : Auditable
{
    public decimal Price { get; set; }
    public DateTime LastDate { get; set; }
    public DateTime NextDate { get; set; }
    public EducationResultDto Education { get; set; }
}