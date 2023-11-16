using Revision.Domain.Configurations;
using Revision.Service.DTOs.SubjectCategories;

namespace Revision.Service.Interfaces.Subjects;

public interface ISubjectCategoryService
{
    Task<SubjectCategoryResultDto> CreateAsync(SubjectCategoryCreationDto dto);
    Task<SubjectCategoryResultDto> UpdateAsync(long id, SubjectCategoryUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> DestroyAsync(long id);
    Task<SubjectCategoryResultDto> GetByIdAsync(long id);
    Task<IEnumerable<SubjectCategoryResultDto>> GetAllAsync();
    Task<IEnumerable<SubjectCategoryResultDto>> GetAllAsync(PaginationParams pagination, string search = null);
}