using Revision.Domain.Configurations;
using Revision.Service.DTOs.UserEducations;

namespace Revision.Service.Interfaces.Educations;

public interface IUserEducationService
{
    Task<UserEducationResultDto> CreateAsync(UserEducationCreationDto dto);
    Task<UserEducationResultDto> GetByUserIdAsync(long userId);
    Task<IEnumerable<UserEducationResultDto>> GetByEducationIdAsync(long educationId);
    Task<IEnumerable<UserEducationResultDto>> GetAllAsync(PaginationParams pagination, string search = null);
}