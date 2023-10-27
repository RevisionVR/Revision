using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;
using System.Collections.ObjectModel;

namespace Revision.Domain.Entities.Categories.EducationCategories;

public class EducationCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public Collection<Education> Educations { get; set; }
}