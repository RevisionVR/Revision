namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentCreationDto
{
    public decimal Price { get; set; }
    public DateTime LastDate { get; set; }
    public DateTime NextDate { get; set; }
    public long EducationId { get; set; }
}