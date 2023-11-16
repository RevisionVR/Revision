using Revision.Domain.Configurations;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.Interfaces.Educations;

public interface IEducationService
{
    Task<EducationResultDto> CreateAsync(EducationCreationDto dto);
    Task<EducationResultDto> UpdateAsync(long id, EducationUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<EducationResultDto> GetByIdAsync(long id);
    Task<IEnumerable<EducationResultDto>> GetAllAsync();
    Task<IEnumerable<EducationResultDto>> GetAllAsync(PaginationParams pagination, string search = null);
}