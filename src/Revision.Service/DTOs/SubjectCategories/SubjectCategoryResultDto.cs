using Revision.Service.DTOs.Subjects;

namespace Revision.Service.DTOs.SubjectCategories;

public class SubjectCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<SubjectResultDto> Subjects { get; set; }
}