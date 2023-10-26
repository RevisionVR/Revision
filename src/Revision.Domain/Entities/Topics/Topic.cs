using Revision.Domain.Commons;
using Revision.Domain.Entities.Subjects;

namespace Revision.Domain.Entities.Topics;

public class Topic : Auditable
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public long SubjectId { get; set; }
    public Subject Subject { get; set; }
}
