namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentCreationDto
{
    public decimal Price { get; set; }
    public int DeviceCount { get; set; }
    public long EducationId { get; set; }
}