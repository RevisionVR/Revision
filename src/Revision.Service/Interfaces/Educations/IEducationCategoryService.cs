using Revision.Domain.Configurations;
using Revision.Service.DTOs.EducationCategories;

namespace Revision.Service.Interfaces.Educations;

public interface IEducationCategoryService
{
    Task<EducationCategoryResultDto> CreateAsync(EducationCategoryCreationDto dto);
    Task<EducationCategoryResultDto> UpdateAsync(long id, EducationCategoryUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> DestroyAsync(long id);
    Task<EducationCategoryResultDto> GetByIdAsync(long id);
    Task<IEnumerable<EducationCategoryResultDto>> GetAllAsync();
    Task<IEnumerable<EducationCategoryResultDto>> GetAllAsync(PaginationParams pagination, string search = null);
}