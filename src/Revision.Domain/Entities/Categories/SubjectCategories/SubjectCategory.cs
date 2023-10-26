using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Categories.SubjectCategories;

public class SubjectCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
}