using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.EducationCategories;

public class EducationCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<EducationResultDto> Educations { get; set; }
}