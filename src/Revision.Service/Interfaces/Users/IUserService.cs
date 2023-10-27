﻿using Revision.Domain.Enums;
using Revision.Service.DTOs.Users;
using Revision.Domain.Configurations;

namespace Revision.Service.Interfaces.Users;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(UserCreationDto dto);
    Task<UserResultDto> UpdateAsync(long id, UserUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<UserResultDto> GetByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> GetAllAsync(PaginationParams pagination);
    Task<UserResultDto> UpgradeRoleAsync(long id, Role role);
}
