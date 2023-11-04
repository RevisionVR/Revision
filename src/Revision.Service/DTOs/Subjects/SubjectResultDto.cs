using Revision.Domain.Commons;
using Revision.Service.DTOs.SubjectCategories;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.DTOs.Subjects;

public class SubjectResultDto : Auditable
{
    public string Name { get; set; } = string.Empty;
    //public AssetResultDto Asset { get; set; } = default;
    public SubjectCategoryResultDto SubjectCategory { get; set; }
    public IEnumerable<TopicResultDto> Topics { get; set; }
}