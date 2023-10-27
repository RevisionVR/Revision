namespace Revision.Service.DTOs.Subjects;

public class SubjectUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public long SubjectCategoryId { get; set; }
}