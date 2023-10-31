using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Subjects;

public class SubjectCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Subject> Subjects { get; set; }
}