﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Enums;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Devices;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Devices;

namespace Revision.Service.Services.Devices;

public class DeviceService : IDeviceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Device> _deviceRepository;
    private readonly IRepository<Education> _educationRepository;
    public DeviceService(
        IMapper mapper,
        IRepository<Device> deviceRepository,
        IRepository<Education> educationRepository)
    {
        _mapper = mapper;
        _deviceRepository = deviceRepository;
        _educationRepository = educationRepository;
    }

    public async Task<DeviceResultDto> CreateAsync(DeviceCreationDto dto)
    {
        var existDevice = await _deviceRepository.SelectAsync(device =>
        device.UniqueId.ToLower().Equals(dto.UniqueId.ToLower()));
        if (existDevice is not null)
            throw new RevisionException(403, "This device already exists");

        var existEducation = await _educationRepository.SelectAsync(
            education => education.Id.Equals(dto.EducationId))
            ?? throw new RevisionException(404, "This education is not found");

        var mappedDevice = _mapper.Map<Device>(dto);
        mappedDevice.Status = DeviceStatus.NoActive;
        mappedDevice.Education = existEducation;
        mappedDevice.CreatedAt = TimeHelper.GetDateTime();

        await _deviceRepository.AddAsync(mappedDevice);
        await _deviceRepository.SaveAsync();

        return _mapper.Map<DeviceResultDto>(mappedDevice);
    }

    public async Task<DeviceResultDto> UpdateAsync(long id, DeviceUpdateDto dto)
    {
        var existDevice = await _deviceRepository.SelectAsync(device => device.Id.Equals(id))
            ?? throw new RevisionException(404, "This device is not found");

        var existEducation = await _educationRepository.SelectAsync(
           education => education.Id.Equals(dto.EducationId))
           ?? throw new RevisionException(404, "This education is not found");

        var mappedDevice = _mapper.Map(dto, existDevice);
        mappedDevice.Id = id;
        mappedDevice.Education = existEducation;
        mappedDevice.Status = DeviceStatus.NoActive;
        mappedDevice.UpdatedAt = TimeHelper.GetDateTime();

        _deviceRepository.Update(mappedDevice);
        await _deviceRepository.SaveAsync();

        return _mapper.Map<DeviceResultDto>(mappedDevice);
    }

    public async Task<DeviceResultDto> UpdateIsActiveAsync(string uniqueId, DeviceStatus status)
    {
        var existDevice = await _deviceRepository.SelectAsync(device =>
        device.UniqueId.ToLower().Equals(uniqueId.ToLower()), includes: new[] { "Education" })
           ?? throw new RevisionException(404, "This device is not found");

        existDevice.Status = status;
        _deviceRepository.Update(existDevice);
        await _deviceRepository.SaveAsync();

        return _mapper.Map<DeviceResultDto>(existDevice);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDevice = await _deviceRepository.SelectAsync(device => device.Id.Equals(id))
           ?? throw new RevisionException(404, "This device is not found");

        _deviceRepository.Delete(existDevice);
        await _deviceRepository.SaveAsync();
        return true;
    }

    public async Task<DeviceResultDto> GetByIdAsync(long id)
    {
        var existDevice = await _deviceRepository.SelectAsync(device => device.Id.Equals(id) &&
        !device.Education.IsDeleted, includes: new[] { "Education" })
           ?? throw new RevisionException(404, "This device is not found");

        return _mapper.Map<DeviceResultDto>(existDevice);
    }

    public async Task<DeviceResultDto> GetByUniqueIdAsync(string uniqueId)
    {
        var existDevice = await _deviceRepository.SelectAsync(device =>
        device.UniqueId.ToLower().Equals(uniqueId.ToLower()), includes: new[] { "Education" })
           ?? throw new RevisionException(404, "This device is not found");

        return _mapper.Map<DeviceResultDto>(existDevice);
    }

    public async Task<IEnumerable<DeviceResultDto>> GetByEducationIdAsync(long educationId)
    {
        var existDevices = await _deviceRepository.SelectAll(device => device.EducationId.Equals(educationId) && 
        !device.Education.IsDeleted, includes: new[] { "Education" })
            .ToListAsync();
        if (!existDevices.Any())
            throw new RevisionException(404, "This education is not found");

        return _mapper.Map<IEnumerable<DeviceResultDto>>(existDevices);
    }

    public async Task<IEnumerable<DeviceResultDto>> GetAllAsync()
    {
        var devices = await _deviceRepository.SelectAll(device => !device.Education.IsDeleted, 
            includes: new[] { "Education" })
            .ToListAsync();

        return _mapper.Map<IEnumerable<DeviceResultDto>>(devices);
    }

    public async Task<IEnumerable<DeviceResultDto>> GetAllAsync(PaginationParams pagination, string search = null)
    {
        var devices = _deviceRepository.SelectAll(device => !device.Education.IsDeleted, 
            includes: new[] { "Education" });
        if (!string.IsNullOrEmpty(search))
        {
            devices = devices.Where(device =>
            device.UniqueId.ToLower().Contains(search.ToLower()));
        }

        var result = devices.ToPagedList(pagination);
        return _mapper.Map<IEnumerable<DeviceResultDto>>(result);
    }
}
