using Revision.Domain.Commons;
using Revision.Domain.Enums;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.Devices;

public class DeviceResultDto : Auditable
{
    public string UniqueId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Glove { get; set; }
    public bool Fragrant { get; set; }
    public DeviceStatus Status { get; set; }
    public EducationResultDto Education { get; set; }
}
