using Revision.Domain.Commons;
using Revision.Service.DTOs.Educations;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.DTOs.TopicPayments;

public class TopicPaymentResultDto : Auditable
{
    public decimal Price { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public TopicResultDto Topic { get; set; }
    public EducationResultDto Education { get; set; }
}