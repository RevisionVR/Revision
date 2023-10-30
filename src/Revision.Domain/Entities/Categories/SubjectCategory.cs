using Revision.Domain.Commons;
using Revision.Domain.Entities.Subjects;

namespace Revision.Domain.Entities.Categories;

public class SubjectCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Subject> Subjects { get; set; }
}