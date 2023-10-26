using Revision.Domain.Commons;
using System.Collections.ObjectModel;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Categories.EducationCategories;

public class EducationCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public Collection<Education> Educations { get; set; }
}