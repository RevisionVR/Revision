using Revision.Service.DTOs.Devices;

namespace Revision.Service.Interfaces.Devices;

public interface IDeviceCountService
{
    Task<DeviceCountDto> GetCountByEducactionIdAsync(long educationId);
    Task<IEnumerable<DeviceCountDto>> GetCountAllAsync();
}