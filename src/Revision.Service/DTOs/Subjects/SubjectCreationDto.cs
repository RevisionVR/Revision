namespace Revision.Service.DTOs.Subjects;

public class SubjectCreationDto
{
    public string Name { get; set; } = string.Empty;
    //public IFormFile Image { get; set; } = null;
    public long SubjectCategoryId { get; set; }
}