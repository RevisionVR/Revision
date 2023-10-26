using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Categories.EducationCategories;

public class EducationCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
}
