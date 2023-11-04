using Revision.Domain.Commons;
using Revision.Service.DTOs.Subjects;
using Revision.Service.DTOs.TopicPayments;

namespace Revision.Service.DTOs.Topics;

public class TopicResultDto : Auditable
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public SubjectResultDto Subject { get; set; }
    public IEnumerable<TopicPaymentResultDto> TopicPayments { get; set; }
}
