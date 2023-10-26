using Revision.Domain.Commons;
using Revision.Domain.Entities.Topics;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Payments;

public class TopicPayment : Auditable
{
    public decimal Price { get; set; }
    public DateTime LastDay { get; set; }
    public DateTime NextDay { get; set; }
    public long TopicId { get; set; }
    public Topic Topic { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}