using Revision.Domain.Configurations;
using Revision.Service.DTOs.Subjects;

namespace Revision.Service.Interfaces.Subjects;

public interface ISubjectService
{
    Task<SubjectResultDto> CreateAsync(SubjectCreationDto dto);
    Task<SubjectResultDto> UpdateAsync(long id, SubjectUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> DestroyAsync(long id);
    Task<SubjectResultDto> GetByIdAsync(long id);
    Task<IEnumerable<SubjectResultDto>> GetBySubjectCategoryIdAsync(long subjectCategoryId);
    Task<IEnumerable<SubjectResultDto>> GetAllAsync();
    Task<IEnumerable<SubjectResultDto>> SearchAsync(string Item);
}