﻿using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Enums;
using Revision.Service.DTOs.Devices;
using Revision.Service.DTOs.Educations;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
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
        var education = await _educationRepository.SelectAsync(education => education.Id.Equals(educationId) && !education.IsDeleted);
        if (education is null)
            throw new RevisionException(404, "This education is not found");

        var existDevices = _deviceRepository.SelectAll(device => device.EducationId.Equals(educationId));
        if (!existDevices.Any())
            throw new RevisionException(404, "This education is not found");

        var result = new DeviceCountDto
        {
            Count = existDevices.Count(),
            Gloves = existDevices.Where(device => device.Glove).Count(),
            Fragrants = existDevices.Where(device => device.Fragrant).Count(),
            Active = existDevices.Where(device => device.Status.Equals(DeviceStatus.Active)).Count(),
            NoActive = existDevices.Where(device => device.Status.Equals(DeviceStatus.NoActive)).Count(),
            Education = _mapper.Map<EducationResultDto>(education)
        };

        return result;
    }

    public async Task<IEnumerable<DeviceCountDto>> GetCountAllAsync()
    {
        var existGroupDevices = _deviceRepository.SelectAll(education => !education.IsDeleted)
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

            deviceCount.Count = educationGroup.Count();
            deviceCount.Gloves = educationGroup.Where(device => device.Glove).Count();
            deviceCount.Fragrants = educationGroup.Where(device => device.Fragrant).Count();
            deviceCount.Active = educationGroup.Where(device => device.Status.Equals(DeviceStatus.Active)).Count();
            deviceCount.NoActive = educationGroup.Where(device => device.Status.Equals(DeviceStatus.NoActive)).Count();

            deviceCount.Education = _mapper.Map<EducationResultDto>(education);

            result.Add(deviceCount);
        }

        return result;
    }

    public async Task<IEnumerable<DeviceCountDto>> GetCountAllAsync(PaginationParams pagination, string search = null)
    {
        var existGroupDevices = _deviceRepository.SelectAll(education => !education.IsDeleted)
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

            deviceCount.Count = educationGroup.Count();
            deviceCount.Gloves = educationGroup.Where(device => device.Glove).Count();
            deviceCount.Fragrants = educationGroup.Where(device => device.Fragrant).Count();
            deviceCount.Active = educationGroup.Where(device => device.Status.Equals(DeviceStatus.Active)).Count();
            deviceCount.NoActive = educationGroup.Where(device => device.Status.Equals(DeviceStatus.NoActive)).Count();

            deviceCount.Education = _mapper.Map<EducationResultDto>(education);

            result.Add(deviceCount);
        }
        if (!string.IsNullOrEmpty(search))
        {
            result = result.Where(device => 
            device.Education.Name.ToLower().Contains(search.ToLower())).ToList();
        }

        var devieCounts = result.AsQueryable().ToPagedList(pagination);
        return devieCounts;
    }
}