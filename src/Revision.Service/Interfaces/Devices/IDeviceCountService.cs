using Revision.Domain.Configurations;
using Revision.Service.DTOs.Devices;

namespace Revision.Service.Interfaces.Devices;

public interface IDeviceCountService
{
    Task<DeviceCountDto> GetCountByEducactionIdAsync(long educationId);
    Task<IEnumerable<DeviceCountDto>> GetCountAllAsync();
    Task<IEnumerable<DeviceCountDto>> GetCountAllAsync(PaginationParams pagination, string search = null);
}