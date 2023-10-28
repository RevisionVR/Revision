using Revision.Domain.Commons;
using Revision.Domain.Entities.Categories;

namespace Revision.Domain.Entities.Subjects;

public class Subject : Auditable
{
    public string Name { get; set; } = string.Empty;

    public long SubjectCategoryId { get; set; }
    public SubjectCategory SubjectCategory { get; set; }
}