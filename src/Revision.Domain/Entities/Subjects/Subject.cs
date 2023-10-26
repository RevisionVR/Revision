using Revision.Domain.Commons;
using Revision.Domain.Entities.Categories.SubjectCategories;

namespace Revision.Domain.Entities.Subjects;

public class Subject : Auditable
{
    public string Name { get; set; }

    public long SubjectCategoryId { get; set; }
    public SubjectCategory SubjectCategory { get; set; }
}