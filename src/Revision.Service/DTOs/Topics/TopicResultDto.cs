using Revision.Service.DTOs.Subjects;
using Revision.Service.DTOs.TopicPayments;

namespace Revision.Service.DTOs.Topics;

public class TopicResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public SubjectResultDto Subject { get; set; }
    public IEnumerable<TopicPaymentResultDto> TopicPayments { get; set; }
}
