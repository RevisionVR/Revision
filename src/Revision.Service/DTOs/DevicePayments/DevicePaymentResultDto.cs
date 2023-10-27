using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentResultDto
{
    public long Id { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public int DeviceCount { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public EducationResultDto Education { get; set; }
}