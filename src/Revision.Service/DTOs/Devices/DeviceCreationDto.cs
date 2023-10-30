namespace Revision.Service.DTOs.Devices;

public class DeviceCreationDto
{
    public string UniqueId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Glove { get; set; }
    public bool Fragrant { get; set; }
    public long EducationId { get; set; }
}