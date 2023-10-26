using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Enums;

namespace Revision.Domain.Entities.Devices;

public class Device : Auditable
{
    public string UniqueId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Glove { get; set; }
    public bool Fragrant { get; set; }
    public DeviceStatus Status {  get; set; }
    
    public long EducationId { get; set; }
    public Education Education { get; set; }
}
