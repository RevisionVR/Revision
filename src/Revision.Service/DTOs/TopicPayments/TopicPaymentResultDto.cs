using Revision.Service.DTOs.Educations;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.DTOs.TopicPayments;

public class TopicPaymentResultDto
{
    public long Id { get; set; }
    public decimal Price { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public TopicResultDto Topic { get; set; }
    public EducationResultDto Education { get; set; }
}