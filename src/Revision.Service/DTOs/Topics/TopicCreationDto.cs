namespace Revision.Service.DTOs.Topics;

public class TopicCreationDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public long SubjectId { get; set; }
}