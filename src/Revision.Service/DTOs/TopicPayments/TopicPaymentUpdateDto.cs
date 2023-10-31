namespace Revision.Service.DTOs.TopicPayments;

public class TopicPaymentUpdateDto
{
    public decimal Price { get; set; }
    public long TopicId { get; set; }
    public long EducationId { get; set; }
}
