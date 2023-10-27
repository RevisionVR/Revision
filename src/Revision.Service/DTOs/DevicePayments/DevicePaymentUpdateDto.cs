namespace Revision.Service.DTOs.DevicePayments;

public class DevicePaymentUpdateDto
{
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public int DeviceCount { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public long EducationId { get; set; }
}
