using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Educations;

public class EducationCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Education> Educations { get; set; }
}