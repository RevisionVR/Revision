using Microsoft.AspNetCore.Http;

namespace Revision.Service.DTOs.Subjects;

public class SubjectUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public IFormFile Image { get; set; }
    public long SubjectCategoryId { get; set; }
}