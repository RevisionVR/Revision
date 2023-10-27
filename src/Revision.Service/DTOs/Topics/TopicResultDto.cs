using Revision.Service.DTOs.Subjects;

namespace Revision.Service.DTOs.Topics;

public class TopicResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public SubjectResultDto Subject { get; set; }
}
