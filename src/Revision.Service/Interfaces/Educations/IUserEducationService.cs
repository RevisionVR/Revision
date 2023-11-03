using Revision.Service.DTOs.UserEducations;

namespace Revision.Service.Interfaces.Educations;

public interface IUserEducationService
{
    Task<UserEducationResultDto> CreateAsync(UserEducationCreationDto dto);
    Task<bool> DeleteAsync(long userEducation);
    Task<UserEducationResultDto> GetByUserIdAsync(long userId);
    Task<IEnumerable<UserEducationResultDto>> GetByEducationIdAsync(long educationId);
}