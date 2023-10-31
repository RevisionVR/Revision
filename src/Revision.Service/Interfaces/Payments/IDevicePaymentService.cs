﻿using Revision.Domain.Configurations;
using Revision.Service.DTOs.DevicePayments;

namespace Revision.Service.Interfaces.Payments;

public interface IDevicePaymentService
{
    Task<DevicePaymentResultDto> CreateAsync(DevicePaymentCreationDto dto);
    Task<DevicePaymentResultDto> GetByIdAsync(long id);
    Task<IEnumerable<DevicePaymentResultDto>> GetAllAsync(PaginationParams pagination);
}