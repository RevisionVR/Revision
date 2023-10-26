using Revision.Domain.Commons;
using System.Collections.ObjectModel;
using Revision.Domain.Entities.Subjects;

namespace Revision.Domain.Entities.Categories.SubjectCategories;

public class SubjectCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public Collection<Subject> Subjects { get; set; }
}