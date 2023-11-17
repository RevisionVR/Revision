namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentCreationDto
{
    public decimal TotalPrice { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public int Count { get; set; }
    public long EducationId { get; set; }
}