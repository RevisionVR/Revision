using Revision.Domain.Commons;
using Revision.Domain.Entities.Subjects;
using System.Collections.ObjectModel;

namespace Revision.Domain.Entities.Categories;

public class SubjectCategory : Auditable
{
    public string Name { get; set; } = string.Empty;
    public Collection<Subject> Subjects { get; set; }
}