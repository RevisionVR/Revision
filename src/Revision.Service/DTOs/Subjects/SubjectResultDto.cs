using Revision.Service.DTOs.SubjectCategories;

namespace Revision.Service.DTOs.Subjects;

public class SubjectResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SubjectCategoryResultDto SubjectCategory { get; set; }
}