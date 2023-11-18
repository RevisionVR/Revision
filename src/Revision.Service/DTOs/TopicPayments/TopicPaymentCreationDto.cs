namespace Revision.Service.DTOs.TopicPayments;

public class TopicPaymentCreationDto
{
    public decimal Price { get; set; }
    public DateTime LastDate { get; set; }
    public DateTime NextDate { get; set; }
    public long TopicId { get; set; }
    public long EducationId { get; set; }
}