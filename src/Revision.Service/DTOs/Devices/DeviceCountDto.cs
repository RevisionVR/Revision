using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.Devices;

public class DeviceCountDto
{
    public int Count { get; set; }
    public int Glove { get; set; }
    public int Fragrant { get; set; }
    public int Active { get; set; }
    public int NoActive { get; set; }
    public EducationResultDto Education { get; set; }
}