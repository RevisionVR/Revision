using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.Devices;

public class DeviceResultDto
{
    public long Id { get; set; }
    public string UniqueId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Glove { get; set; }
    public bool Fragrant { get; set; }
    public bool IsActive { get; set; }
    public EducationResultDto Education { get; set; }
}
