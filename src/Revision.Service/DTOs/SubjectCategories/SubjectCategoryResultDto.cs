using Revision.Domain.Commons;
using Revision.Service.DTOs.Subjects;

namespace Revision.Service.DTOs.SubjectCategories;

public class SubjectCategoryResultDto : Auditable
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<SubjectResultDto> Subjects { get; set; }
}