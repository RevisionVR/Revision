using Revision.Domain.Commons;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.DTOs.Devices;

public class DeviceCountDto : Auditable
{
    public int Count { get; set; }
    public int Gloves { get; set; }
    public int Fragrants { get; set; }
    public int Active { get; set; }
    public int NoActive { get; set; }
    public EducationResultDto Education { get; set; }
}