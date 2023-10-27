namespace Revision.Service.DTOs.TopicPayments;

public class TopicPaymentUpdateDto
{
    public decimal Price { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public long TopicId { get; set; }
    public long EducationId { get; set; }
}
