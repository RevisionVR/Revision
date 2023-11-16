using Revision.Domain.Configurations;
using Revision.Domain.Enums;
using Revision.Service.DTOs.Users;

namespace Revision.Service.Interfaces.Users;

public interface IUserService
{
    //Task<UserResultDto> CreateAsync(UserCreationDto dto);
    Task<UserResultDto> UpdateAsync(long id, UserUpdateDto dto);
    Task<UserResultDto> UpgradeRoleAsync(long id, Role role);
    Task<bool> DeleteAsync(long id);
    Task<UserResultDto> GetByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> GetByRoleAsync(Role role);
    Task<UserResultDto> UpdateSecurityAsync(long id, UserSecurityUpdateDto security);
    Task<IEnumerable<UserResultDto>> GetAllAsync(PaginationParams pagination, string search = null);
}