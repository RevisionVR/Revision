using Revision.Domain.Configurations;
using Revision.Domain.Enums;
using Revision.Service.DTOs.Devices;

namespace Revision.Service.Interfaces.Devices;

public interface IDeviceService
{
    Task<DeviceResultDto> CreateAsync(DeviceCreationDto dto);
    Task<DeviceResultDto> UpdateAsync(long id, DeviceUpdateDto dto);
    Task<DeviceResultDto> UpdateIsActiveAsync(string uniqueId, DeviceStatus status);
    Task<bool> DeleteAsync(long id);
    Task<DeviceResultDto> GetByIdAsync(long id);
    Task<DeviceResultDto> GetByUniqueIdAsync(string uniqueId);
    Task<IEnumerable<DeviceResultDto>> GetByEducationIdAsync(long educationId);
    Task<IEnumerable<DeviceResultDto>> GetAllAsync();
}