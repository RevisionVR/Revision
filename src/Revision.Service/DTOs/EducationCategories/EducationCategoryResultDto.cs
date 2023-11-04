using Revision.Domain.Commons;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.EducationCategories;

public class EducationCategoryResultDto : Auditable
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<EducationResultDto> Educations { get; set; }
}