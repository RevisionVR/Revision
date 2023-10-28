namespace Revision.Service.DTOs.Educations;

public class EducationUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public int? Number { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; }
    public long UserId { get; set; }
    public long EducationCategoryId { get; set; }
}