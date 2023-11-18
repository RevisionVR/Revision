using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Topics;

namespace Revision.Domain.Entities.Payments;

public class TopicPayment : Auditable
{
    public decimal Price { get; set; }
    public DateTime LastDate { get; set; }
    public DateTime NextDate { get; set; }

    public long TopicId { get; set; }
    public Topic Topic { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}