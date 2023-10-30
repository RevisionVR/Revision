using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Devices;

public class Device : Auditable
{
    public string UniqueId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Glove { get; set; }
    public bool Fragrant { get; set; }
    public bool IsActive { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}
