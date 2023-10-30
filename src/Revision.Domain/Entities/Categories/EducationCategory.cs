using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Categories;

public class EducationCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Education> Educations { get; set; }
}