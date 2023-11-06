using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Service.DTOs.Devices;
using Revision.Service.DTOs.Educations;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Devices;

namespace Revision.Service.Services.Devices;

public class DeviceCountService : IDeviceCountService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Device> _deviceRepository;
    private readonly IRepository<Education> _educationRepository;
    public DeviceCountService(
        IMapper mapper,
        IRepository<Device> deviceRepository, 
        IRepository<Education> educationRepository)
    {
        _mapper = mapper;
        _deviceRepository = deviceRepository;
        _educationRepository = educationRepository;
    }

    public async Task<DeviceCountDto> GetCountByEducactionIdAsync(long educationId)
    {
        var education = await _educationRepository.SelectAsync(education => education.Id.Equals(educationId));
        if (education is null)
            throw new RevisionException(404, "This education is not found");

        var existDevices = _deviceRepository.SelectAll(device => device.EducationId.Equals(educationId));
        if (!existDevices.Any())
            throw new RevisionException(404, "This education is not found");

        var result = new DeviceCountDto
        {
            Count = existDevices.Count(),
            Glove = existDevices.Where(device => device.Glove).Count(),
            Fragrant = existDevices.Where(device => device.Fragrant).Count(),
            Education = _mapper.Map<EducationResultDto>(education)
        };

        return result;
    }

    public async Task<IEnumerable<DeviceCountDto>> GetCountAllAsync()
    {
        var existGroupDevices = _deviceRepository.SelectAll()
            .AsEnumerable()
            .GroupBy(e => e.EducationId);

        var result = new List<DeviceCountDto>();

        foreach (var educationGroup in existGroupDevices)
        {
            var educationId = educationGroup.Key;
            var education = await _educationRepository.SelectAsync(e => e.Id.Equals(educationId));
            if (education is null)
                continue;

            var deviceCount = new DeviceCountDto();
            foreach (var device in educationGroup)
            {
                if (device is null)
                    continue;

                deviceCount.Count += 1;

                if (device.Fragrant)
                    deviceCount.Fragrant += 1;

                if (device.Glove)
                    deviceCount.Glove += 1;

            }

            deviceCount.Education = _mapper.Map<EducationResultDto>(education);
            result.Add(deviceCount);
        }

        return result;
    }
}